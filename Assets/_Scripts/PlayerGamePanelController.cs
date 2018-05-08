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
        UpdateRoundsWonIndicator();
    }

    void UpdateRoundsWonIndicator()
    {
        if (GameManager.players[playerNumber - 1].wins == 1)
        {
            firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
        }
        else if (GameManager.players[playerNumber - 1].wins == 2)
        {
            firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
            secondRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
        }
        else if (GameManager.players[playerNumber - 1].wins == 3)
        {
            firstRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
            secondRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
            thirdRoundWinIndicator.color = GameManager.players[playerNumber - 1].playerColor;
        }
    }
    
}
