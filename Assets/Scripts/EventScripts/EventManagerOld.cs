using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManagerOld : MonoBehaviour {

    public delegate void EventFunctions(); // event container
    public static event EventFunctions EventUpdate;//event under the EventFunction container
    public static event EventFunctions CellEvents;
    public static event EventFunctions SpawnEvents;

    public delegate void EventSpawns(Vector3 spawnPoint, Transform endPoint);
    public static event EventSpawns OnCellSpawn;
    public static event EventSpawns OnObjectSpawn;


    public delegate void EventDespawn(Transform transform);
    public static event EventDespawn OnDespawnObject;
    public static event EventDespawn OnDespawnCell;
    
    public static void CallDespawnCell(Transform t)
    {
        if (OnDespawnCell != null)
            OnDespawnCell(t);
    }

    public static void CallDespawnObject(Transform t)
    {
        if (OnDespawnObject != null)
            OnDespawnObject(t);
    }

    public static void CallCellSpawn(Vector3 sp, Transform ep)
    {
        if (OnCellSpawn != null)
            OnCellSpawn(sp, ep);
    }

    public static void CallObjectSpawn(Vector3 sp, Transform ep)
    {
        if (OnObjectSpawn != null)
            OnObjectSpawn(sp, ep);
    }

    public static void CallCellEvents()
    {
        if (CellEvents != null)
            CellEvents();
    }

    public static void CallSpawnEvents()
    {
        if (SpawnEvents != null)
            SpawnEvents();
    }

    public static void CallEvents()
    {
        if (EventUpdate !=null)
        {
            EventUpdate();
        }
    }

    public void Update()
    {
        CallEvents();
    }

}

