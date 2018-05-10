using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;
using Prototype.NetworkLobby;

public class GameManager : NetworkBehaviour
{
    static public GameManager instance;

    static public List<PlayerManager> players = new List<PlayerManager>();

    [HideInInspector] [SyncVar] public bool m_GameIsFinished = false;

    [SerializeField] int numRoundsToWin = 3;
    [SerializeField] Text messageText;
    [SerializeField] CameraController cameraControl;
    [SerializeField] GameObject playerPrefab;
    [SerializeField] CameraController mainCamera;
    [SerializeField] LiftRampsAfterStart[] allRamps;
    [SerializeField] DropRingAfterTime[] allStagePieces;
    [SerializeField] Transform[] allSpawnPoints;

    //public GameObject[] PlayerControls;
    //[SerializeField] PlayerManager[] players;
    //public PlayerManager[] Players
    //{
    //    get { return players; }
    //}

    [Space]
    [Header("UI")]
    //public CanvasGroup fadingScreen;
    public CanvasGroup endRoundScreen;

    float startDelay = 3f;
    float endDelay = 3f;
    
    private int roundNumber;
    private WaitForSeconds startWait;
    private WaitForSeconds endWait;
    private PlayerManager roundWinner;
    private PlayerManager gameWinner;

    void Awake()
    {
        instance = this;
    }

    [ServerCallback]
    private void Start()
    {
        //informationNeededFromMenu = GameObject.Find("InformationForGameManager").GetComponent<InformationFromMenuToGameManager>();
        startWait = new WaitForSeconds(startDelay);
        endWait = new WaitForSeconds(endDelay);

        //SpawnAllPlayers();

        StartCoroutine(GameLoop());
    }

    static public void AddPlayer(GameObject player, int playerNum, Color c, string name, int localID)
    {
        PlayerManager tmp = new PlayerManager();
        tmp.instance = player;
        tmp.playerNumber = playerNum;
        tmp.playerColor = c;
        tmp.playerName = name;
        tmp.localPlayerID = localID;
        tmp.Setup();
        tmp.spawnPoint = instance.allSpawnPoints[playerNum - 1];

        players.Add(tmp);
    }

    public void RemovePlayer(GameObject Player)
    {
        PlayerManager toRemove = null;
        foreach (var tmp in players)
        {
            if (tmp.instance == Player)
            {
                toRemove = tmp;
                break;
            }
        }

        if (toRemove != null)
            players.Remove(toRemove);
    }

    //private void SpawnAllPlayers()
    //{
    //    for (int i = 0; i < players.Count; i++)
    //    {
    //        players[i].instance =
    //            Instantiate(playerPrefab, players[i].spawnPoint.transform.position, players[i].spawnPoint.transform.rotation) as GameObject;
    //        players[i].playerNumber = i + 1;
    //        players[i].Setup();
    //    }
    //}

    private IEnumerator GameLoop()
    {
        while (players.Count < LobbyManager.s_Singleton.numPlayers)
            yield return null;

        //yield return new WaitForSeconds(2.0f);

        yield return StartCoroutine(RoundStarting());
        yield return StartCoroutine(RoundPlaying());
        yield return StartCoroutine(RoundEnding());

        if (gameWinner != null)
        {
            m_GameIsFinished = true;
            float leftWaitTime = 15.0f;
            bool allAreReady = false;
            int flooredWaitTime = 15;

            while (leftWaitTime > 0.0f && !allAreReady)
            {
                yield return null;

                allAreReady = true;
                foreach (var tmp in players)
                {
                    allAreReady &= tmp.IsReady();
                }

                leftWaitTime -= Time.deltaTime;

                int newFlooredWaitTime = Mathf.FloorToInt(leftWaitTime);

                if (newFlooredWaitTime != flooredWaitTime)
                {
                    flooredWaitTime = newFlooredWaitTime;
                    string message = EndMessage(flooredWaitTime);
                    RpcUpdateMessage(message);
                }
            }

            LobbyManager.s_Singleton.ServerReturnToLobby();
            //SceneManager.LoadScene(0);
        }
        else
        {
            StartCoroutine(GameLoop());
        }
    }


    private IEnumerator RoundStarting()
    {
        //we notify all clients that the round is starting
        RpcRoundStarting();

        // Wait for the specified length of time until yielding control back to the game loop.
        yield return startWait;
    }

    [ClientRpc]
    void RpcRoundStarting()
    {
        // As soon as the round starts reset the players and make sure they can't move.
        ResetAllPlayers();
        ResetAllRamps();
        ResetAllStagePieces();
        DisablePlayerControl();

        // Snap the camera's zoom and position to something appropriate for the reset tanks.
        cameraControl.ResetCamera();

        // Increment the round number and display text showing the players what round it is.
        roundNumber++;
        messageText.text = "ROUND " + roundNumber;


        StartCoroutine(ClientRoundStartingFade());
    }

    private IEnumerator ClientRoundStartingFade()
    {
        float elapsedTime = 0.0f;
        float wait = startDelay - 0.5f;

        yield return null;

        while (elapsedTime < wait)
        {
            if (roundNumber != 1)
                endRoundScreen.alpha = 1.0f - (elapsedTime / wait);
            //break; ;//fadingScreen.alpha = 1.0f - (elapsedTime / wait);
            //else


            elapsedTime += Time.deltaTime;

            //sometime, synchronization lag behind because of packet drop, so we make sure our tank are reseted
            if (elapsedTime / wait < 0.5f)
            {
                ResetAllPlayers();
                Debug.Log("Reset all players");
            }

            yield return null;
        }
    }


    private IEnumerator RoundPlaying()
    {
        RpcRoundPlaying();

        while (!OnePlayerLeft())
        {
            yield return null;
        }
    }

    [ClientRpc]
    void RpcRoundPlaying()
    {
        MakeAllPlayersNotKinimetic();
        //EnablePlayerControl();
        StartCoroutine(WaitToEnablePlayers());

        // Clear the text from the screen.
        messageText.text = string.Empty;
    }

    IEnumerator WaitToEnablePlayers()
    {
        yield return new WaitForSeconds(1f);

        EnablePlayerControl();
    }

    void MakeAllPlayersNotKinimetic()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].MakeNotKinematic();
        }
    }

    private IEnumerator RoundEnding()
    {
        //DisablePlayerControl();

        roundWinner = null;

        roundWinner = GetRoundWinner();

        if (roundWinner != null)
            roundWinner.wins++;

        gameWinner = GetGameWinner();

        RpcUpdateMessage(EndMessage(0));

        RpcRoundEnding();


        yield return endWait;
    }

    [ClientRpc]
    private void RpcRoundEnding()
    {
        DisablePlayerControl();
        StartCoroutine(ClientRoundEndingFade());
    }

    [ClientRpc]
    private void RpcUpdateMessage(string msg)
    {
        messageText.text = msg;
    }

    private IEnumerator ClientRoundEndingFade()
    {
        float elapsedTime = 0.0f;
        float wait = endDelay;
        while (elapsedTime < wait)
        {
            endRoundScreen.alpha = (elapsedTime / wait);

            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
    
    private bool OnePlayerLeft()
    {
        int numPlayersLeft = 0;

        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].instance.activeSelf)
                numPlayersLeft++;
        }

        return numPlayersLeft <= 1;
    }

    void LockPlayersYAxis()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].LockYAxis();
        }
    }

    private PlayerManager GetRoundWinner()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].instance.activeSelf)
                return players[i];
        }

        return null;
    }


    private PlayerManager GetGameWinner()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].wins == numRoundsToWin)
                return players[i];
        }

        return null;
    }


    private string EndMessage(int waitTime)
    {
        string message = "DRAW!";

        if (gameWinner != null)
            message = gameWinner.coloredPlayerText + " WINS THE GAME!";
        else if (roundWinner != null)
            message = roundWinner.coloredPlayerText + " WINS THE ROUND!";

        message += "\n\n";

        for (int i = 0; i < players.Count; i++)
        {
            message += players[i].coloredPlayerText + ": " + players[i].wins + " WINS\n";
        }

        if (gameWinner != null)
            message += "\n\n<size=20 > Return to lobby in " + waitTime + "\nPress Fire to get ready</size>";

        return message;
    }


    private void ResetAllPlayers()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].Reset();
        }
    }

    private void ResetAllRamps()
    {
        for (int i = 0; i < players.Count; i++)
        {
            allRamps[i].Reset();
        }
    }
    private void ResetAllStagePieces()
    {
        for (int i = 0; i < players.Count; i++)
        {
            allStagePieces[i].Reset();
        }
    }

    private void EnablePlayerControl()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].EnableControl();
        }
    }


    private void DisablePlayerControl()
    {
        for (int i = 0; i < players.Count; i++)
        {
            players[i].DisableControl();
        }
    }
}
