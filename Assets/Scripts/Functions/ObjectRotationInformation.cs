using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotationInformation : MonoBehaviour
{

    RunTimeConsoleText rtcText;
    Vector3 axisCheck;
    [SerializeField]
    Transform childTest;
    [SerializeField]
    Transform raycasterObject;
    [SerializeField]
    Transform blueObjectPrefab;

    Transform clone;

    Vector3 cellPos;
    Vector3 cellDirection;
    float spawnDistance = 10;

    Vector3 spawnPos;

    // Use this for initialization
    void Start ()
    {

        rtcText = GameObject.Find("RuntimeConsoleText").GetComponent<RunTimeConsoleText>();

        InvokeRepeating("ReOrientForSpawning", 0f, 2f);

        childTest.transform.LookAt(Camera.main.transform);

	}
	
	// Update is called once per frame
	void Update ()
    {

        rtcText.consoleDebugString = transform.eulerAngles.x.ToString() + ", " + transform.eulerAngles.y.ToString() + ", " + transform.eulerAngles.z.ToString();

        Vector3 forward = raycasterObject.TransformDirection(Vector3.forward) * 15;

        Debug.DrawRay(raycasterObject.position, forward, Color.red);

    }

    void ReOrientForSpawning()
    {

        float xAxis = 0.0f;
        float xAxisAdjusted;
        float yAxis = 0.0f;
        float yAxisAdjusted;

        float childTestxAxisAdjusted;
        float childTestyAxisAdjusted;

        xAxis = Random.Range(20f, 340f);
        xAxisAdjusted = xAxis + 360f;
        yAxis = Random.Range(10f, 320f);
        yAxisAdjusted = yAxis + 360f;

        childTestxAxisAdjusted = childTest.eulerAngles.x + 360f;
        childTestyAxisAdjusted = childTest.eulerAngles.y + 360f;

        while (xAxisAdjusted > childTestxAxisAdjusted - 5f && xAxisAdjusted < childTestxAxisAdjusted + 5f)
        {
            xAxis = Random.Range(20f, 340f);
            xAxisAdjusted = xAxis + 360f;
        }

        while (yAxisAdjusted > childTestyAxisAdjusted - 5f && yAxisAdjusted < childTestyAxisAdjusted + 5f)
        {
            yAxis = Random.Range(10f, 320f);
            yAxisAdjusted = yAxis + 360f;
        }

        raycasterObject.eulerAngles = new Vector3(xAxis, yAxis, 0);
        SpawnTester();

    }

    void SpawnTester()
    {
        //new Vector3(hitPoint.x, hitPoint.y + 15, hitPoint.z)

        cellPos = raycasterObject.position;
        cellDirection = raycasterObject.forward;

        spawnPos = cellPos + cellDirection * spawnDistance;

        clone = Instantiate(blueObjectPrefab, spawnPos, Quaternion.identity);
        clone.GetComponent<SpawnedObjectMovement>().EndPos = transform.position;

    }

}
