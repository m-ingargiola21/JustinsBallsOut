using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class KillPlayersThatHitLava : NetworkBehaviour
{

    void OnTriggerEnter(Collider other)
    {
        if (!isServer)
            return;

        if (other.gameObject.tag == "Player")
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage();
    }
}
