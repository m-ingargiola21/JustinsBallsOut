using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour, ISubscriber
{
    [SerializeField] float raycastDistanceMax = 40f;
    [SerializeField] float raycastDistanceMin = 5f;
    [TooltipAttribute("Layer for objects that the raycaster should hit in order to avoid double placing objects")]
    [SerializeField] int layerForRaycasting;
    [SerializeField] Transform rotator;
    [SerializeField] bool isEasyMode;

    float xRandom;
    float yRandom;
    float zRandom;
    float distanceRandom;
    Vector3 rayDirection;
    RaycastHit hit;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    void Start ()
    {
        layerForRaycasting = 1 << layerForRaycasting;
	}

    void RaycastToSpawnFood(Vector3 v)
    {
        bool isOverLapping = false;

        do
        {

            if (!isEasyMode)
            {
                isOverLapping = HardMode();
            }
            else
            {
                isOverLapping = EasyMode();
            }

        } while (isOverLapping);
    }

    bool HardMode()
    {
        Vector3 spawnPoint;

        do
        {
            xRandom = Random.Range(0f, 359.9f);
        } while (xRandom > 60 && xRandom < 90 || xRandom > 270 && xRandom < 300);
        yRandom = Random.Range(0f, 359.9f);
        zRandom = Random.Range(0f, 359.9f);
        distanceRandom = Random.Range(raycastDistanceMin, raycastDistanceMax);

        rayDirection = new Vector3(xRandom, yRandom, zRandom);

        rotator.eulerAngles = rayDirection;
        
        if (!Physics.CheckSphere(rotator.position + rotator.forward * distanceRandom, .7f, layerForRaycasting))
        {
            spawnPoint = rotator.position + rotator.forward * distanceRandom;
            
            EventManager.CallRaycastAction(spawnPoint);
            return false;
        }
        else
            return true;
    }

    bool EasyMode()
    {
        Vector3 spawnPoint;
        
        xRandom = Random.Range(-15f, 15f);
        yRandom = Random.Range(-20f, 20f);
        zRandom = Random.Range(-20f, 20f);
        distanceRandom = Random.Range(raycastDistanceMin + 5f, raycastDistanceMax + 10f);

        rayDirection = new Vector3(xRandom, yRandom, zRandom);

        rotator.eulerAngles = rayDirection;

        if (!Physics.CheckSphere(rotator.position + rotator.forward * distanceRandom, .7f, layerForRaycasting))
        {
            spawnPoint = rotator.position + rotator.forward * distanceRandom;

            EventManager.CallRaycastAction(spawnPoint);
            return false;
        }
        else
            return true;
    }

    public void Subscribe()
    {
        EventManager.OnTimerAction += RaycastToSpawnFood;
    }

    public void Unsubscribe()
    {
        EventManager.OnTimerAction -= RaycastToSpawnFood;
    }

}
