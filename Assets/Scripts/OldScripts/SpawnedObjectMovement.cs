using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedObjectMovement : MonoBehaviour
{

    [SerializeField] float lerpTime = 5f;
    float currentLerpTime;
    bool shouldMove = true;
    Vector3 startPos;
    Vector3 endPos;
    Vector3 positionAdjusted;
    Transform transformPlaceholder;
    bool isMoving;
    MeshRenderer meshRenderer;

    public float LerpTime
    { get { return lerpTime; } set { lerpTime = value; } }

    public Vector3 EndPos
    { get { return endPos; } set { endPos = value; } }

    // Use this for initialization
    void Start ()
    {
        isMoving = true;
        startPos = transform.position;
        meshRenderer = GetComponent<MeshRenderer>();
        transformPlaceholder = GameObject.Find("TransformPlaceholder").transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        IsObjectSeenByCamera();

        if (shouldMove)// if shouldmove == true
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;

            float t = currentLerpTime / lerpTime;
            t = (t * t * t * (t * (6f * t - 15f) + 10f));

            transform.position = Vector3.Lerp(startPos, endPos, t);

            isMoving = true;
        }
	}
    /// <summary> StartMovement:
    /// overload:: includes an end position
    /// This shouldn't be needed since the endPos should never change... the cells are stationary.
    /// </summary>
    /// <param name="cellPrefabTransformPosition"></param>
    public void StartMovement(Vector3 cellPrefabTransformPosition)
    {
        endPos = cellPrefabTransformPosition;
        shouldMove = true;
    }

    /// <summary> StartMovement:
    /// sets bool to true so object can move when looked at
    /// </summary>
    public void StartMovement()
    {
        shouldMove = true;
    }

    /// <summary> StopMovement:
    /// sets bool to false so object stops moving when it is not looked at.
    /// </summary>
    public void StopMovement()
    {
        shouldMove = false;
    }

    /// <summary> IsCellSeenByCamera:
    /// determines if the objects are seen by the player and resumes or stops object movement accordingly.
    /// </summary>
    private void IsObjectSeenByCamera()
    {
        float headingTowardObject;
        float cameraHeading = Camera.main.transform.eulerAngles.y;
        
        headingTowardObject = CheckSpawnedObjectHeading();

        if (Mathf.Abs(Mathf.DeltaAngle(headingTowardObject, cameraHeading)) < 45f)// if degree difference between camera direction and object heading is less than 45
        {
            StartMovement();
        }
        else
        {
            StopMovement();
        }
    }

    /// <summary> CheckSpawnedObjectHeading:
    /// Takes the passed in Trasform and changes its forward to be y=0;
    /// </summary>
    /// <param name="clone"></param>
    private float CheckSpawnedObjectHeading()
    {
        positionAdjusted = transform.position;
        positionAdjusted.y = 0;
        transformPlaceholder.LookAt(positionAdjusted);
        //runTimeConsoleText.consoleDebugString = transformPlaceholder.eulerAngles.y.ToString();

        return transformPlaceholder.eulerAngles.y;

    }
}
