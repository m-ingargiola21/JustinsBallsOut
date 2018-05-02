using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastVisualizer : MonoBehaviour
{
    [SerializeField]
    GameObject spawnCenterView;
    [SerializeField]
    GameObject spawnLeftView;
    [SerializeField]
    GameObject spawnRightView;
    [SerializeField]
    RunTimeConsoleText runTimeConsoleText;
    [SerializeField]
    Transform greenObjectPrefab;
    [SerializeField]
    Transform redObjectPrefab;
    [SerializeField]
    Transform blueObjectPrefab;

    public float fRandom;
    float frandomCheck;
    bool isNewSpot;

    float timer = 0f;
    float timeBetweenSpawns = 5f;
    int layerMask = 1 << 2;
    
    // Use this for initialization
    void Start ()
    {

        fRandom = Random.Range(-45f, 46f);
        frandomCheck = fRandom;
        isNewSpot = true;
        layerMask = ~layerMask;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward) * 20;
        Debug.DrawRay(transform.position, forward, Color.green);
        Vector3 left = (spawnLeftView.transform.position - transform.position);
        Debug.DrawRay(transform.position, left, Color.green);
        Vector3 right = (spawnRightView.transform.position - transform.position);
        Debug.DrawRay(transform.position, right, Color.green);



        Vector3 vRandom = Quaternion.Euler(0, fRandom, 0) * forward;
        Debug.DrawRay(transform.position, vRandom, Color.red);

        RaycastHit hit;
        Physics.Raycast(transform.position, vRandom, out hit, 25f, layerMask);

        //runTimeConsoleText.consoleDebugString = hit.transform.name;
        

        if ((timer % timeBetweenSpawns) >= timeBetweenSpawns - .04f && !isNewSpot)
        {

            if (fRandom == frandomCheck)
            {
                fRandom = Random.Range(-45f, 45f);

                timeBetweenSpawns -= .1f;
                if (timeBetweenSpawns < 1.5f)
                    timeBetweenSpawns = 1.5f;
            }
            
        }
        
        if (isNewSpot)
        {
            switch (hit.transform.name)
            {
                case "RedSpawnArea":
                    Instantiate(redObjectPrefab, new Vector3(hit.point.x, hit.point.y + 15, hit.point.z), Quaternion.identity);
                    break;
                case "BlueSpawnArea":
                    Instantiate(blueObjectPrefab, new Vector3(hit.point.x, hit.point.y + 15, hit.point.z), Quaternion.identity);
                    break;
                case "GreenSpawnArea":
                    Instantiate(greenObjectPrefab, new Vector3(hit.point.x, hit.point.y + 15, hit.point.z), Quaternion.identity);
                    break;
            }
            isNewSpot = false;
        }

        if (fRandom == frandomCheck)
        {
            if ((timer % timeBetweenSpawns) <= .05f)
                isNewSpot = false;
        }
        else
        {
            isNewSpot = true;
            frandomCheck = fRandom;
        }

        //Timer();
    }

    void Timer()
    {
        timer += Time.deltaTime;
        //runTimeConsoleText.consoleDebugString = (timer % timeBetweenSpawns).ToString();
    }
}
