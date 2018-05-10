using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PlayerGamePanelController : NetworkBehaviour
{
    [SerializeField] int playerNumber;
    [SerializeField] Image firstRoundWinIndicator;
    [SerializeField] Image secondRoundWinIndicator;
    [SerializeField] Image thirdRoundWinIndicator;

    void Update()
    {
        if (!isServer)
            return;

        UpdateRoundsWonIndicator();
    }

    [ServerCallback]
    void UpdateRoundsWonIndicator()
    {
        RoundsWondUIUpdate();
        RpcUpdateRoundsWonIndicator();
    }

    [ClientRpc]
    public void RpcUpdateRoundsWonIndicator()
    {
        RoundsWondUIUpdate();
    }

    private void RoundsWondUIUpdate()
    {
        if(GameManager.players.Count >= playerNumber)
        {
            switch (GameManager.players[playerNumber - 1].wins)
            {
                case 1:
                    firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    break;
                case 2:
                    firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    secondRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    break;
                case 3:
                    firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    secondRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    thirdRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
                    break;
                default:
                    break;
            }
        }
    }
    
}
