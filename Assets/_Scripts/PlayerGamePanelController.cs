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
        RpcUpdateRoundsWonIndicator();
    }

    void UpdateRoundsWonIndicator()
    {
        RoundsWondUIUpdate();
    }

    [ClientRpc]
    public void RpcUpdateRoundsWonIndicator()
    {
        RoundsWondUIUpdate();
    }

    private void RoundsWondUIUpdate()
    {
        switch(GameManager.players[playerNumber - 1].wins)
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
