using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhatAmICollidingWith : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
    }

}
