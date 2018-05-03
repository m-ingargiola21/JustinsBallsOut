﻿using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class PlayerManager: NetworkLobbyPlayer
{
    [SyncVar]
    public Color playerColor;
    [SyncVar]
    public Transform spawnPoint;
    [HideInInspector][SyncVar]
    public int playerNumber;
    [HideInInspector][SyncVar]
    public string coloredPlayerText;
    [HideInInspector][SyncVar]
    public GameObject instance;
    [HideInInspector]
    public int wins;


    private PlayerMovement movement;
    //private GameObject canvasGameObject;


    public void Setup()
    {
        movement = instance.GetComponent<PlayerMovement>();
        //canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = playerNumber;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }
    }

    public void LockYAxis()
    {
        instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

    public void UnlockYAxis()
    {
        instance.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }

    public void DisableControl()
    {
        movement.enabled = false;

        //canvasGameObject.SetActive(false);
    }

    public void MakeNotKinematic()
    {
        instance.GetComponent<Rigidbody>().isKinematic = false;
    }

    public void EnableControl()
    {
        movement.enabled = true;

        //canvasGameObject.SetActive(true);
    }
    
    public void Reset()
    {
        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}