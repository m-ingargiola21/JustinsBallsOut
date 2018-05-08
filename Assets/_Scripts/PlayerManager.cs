using System;
using UnityEngine;
using UnityEngine.Networking;

[Serializable]
public class PlayerManager
{
    public Color playerColor;
    public Transform spawnPoint;
    [HideInInspector] public int playerNumber;
    [HideInInspector] public string coloredPlayerText;
    [HideInInspector] public string playerName;
    [HideInInspector] public int localPlayerID;
    [HideInInspector] public GameObject instance;
    [HideInInspector] public int wins;


    private PlayerMovement movement;
    public PlayerSetup setup;
    //private GameObject canvasGameObject;


    public void Setup()
    {
        movement = instance.GetComponent<PlayerMovement>();
        setup = instance.GetComponent<PlayerSetup>();
        //canvasGameObject = instance.GetComponentInChildren<Canvas>().gameObject;

        movement.playerNumber = playerNumber;
        movement.localID = localPlayerID;

        coloredPlayerText = "<color=#" + ColorUtility.ToHtmlStringRGB(playerColor) + ">PLAYER " + playerNumber + "</color>";

        MeshRenderer[] renderers = instance.GetComponentsInChildren<MeshRenderer>();

        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = playerColor;
        }

        setup.m_Color = playerColor;
        setup.m_PlayerName = playerName;
        setup.m_PlayerNumber = playerNumber;
        setup.m_LocalID = localPlayerID;
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

    public string GetName()
    {
        return setup.m_PlayerName;
    }

    public void SetLeader(bool leader)
    {
        setup.SetLeader(leader);
    }

    public bool IsReady()
    {
        return setup.m_IsReady;
    }

    public void Reset()
    {
        movement.SetDefaults();

        instance.transform.position = spawnPoint.position;
        instance.transform.rotation = spawnPoint.rotation;

        instance.SetActive(false);
        instance.SetActive(true);
    }
}