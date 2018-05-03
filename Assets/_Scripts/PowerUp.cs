using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public enum PowerUpTypes { OilSlick, AirControl, MetalBall, SuperJump };

    PowerUpTypes myType;
    PlayerMovement playerMovement;

    private void OnEnable()
    {
        myType = (PowerUpTypes)Random.Range(0, 4);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.transform.GetComponent<PlayerMovement>();

            if(!playerMovement.HasPowerUp)
            {
                playerMovement.GetPowerUp(myType);

                Destroy(this);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            playerMovement = other.transform.GetComponent<PlayerMovement>();

            if (!playerMovement.HasPowerUp)
            {
                playerMovement.GetPowerUp(myType);

                Destroy(this);
            }
        }
    }
}
