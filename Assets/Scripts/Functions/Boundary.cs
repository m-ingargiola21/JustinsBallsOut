using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    bool isFood = false;

    private void OnTriggerEnter(Collider other)
    {
        GameObject collisionGo = other.gameObject;

        if (collisionGo.GetComponent<BulletNew>())
        {
            EventManager.CallBulletMissed(collisionGo, collisionGo.GetComponent<BulletNew>().HasHitFood);
        }
    }

}
