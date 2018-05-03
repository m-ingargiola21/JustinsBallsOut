using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    Transform startCameraPoint;
    [SerializeField]
    Transform gamePlayCameraPoint;

    float cameraMoveSpeed = 5.0f;
    float cameraRotationSpeed = .25f;
    float startTime;
    float distanceToMoveCamera;
    float timeToWait = 3.0f;
    bool hasFinishedWaiting = false;

	void Start ()
    {
        ResetCamera();
	}
	
	void Update ()
    {

    }

    IEnumerator WaitForRoundToStart()
    {
        float distanceCovered;
        float rotationCovered;
        float fractionOfDistance;

        yield return new WaitForSeconds(timeToWait);

        hasFinishedWaiting = true;
        startTime = Time.time;

        while (transform.position != gamePlayCameraPoint.position && transform.rotation != gamePlayCameraPoint.rotation)
        {
            distanceCovered = (Time.time - startTime) * cameraMoveSpeed;
            rotationCovered = (Time.time - startTime) * cameraRotationSpeed;
            fractionOfDistance = distanceCovered / distanceToMoveCamera;

            if (fractionOfDistance > 1)
                fractionOfDistance = 1;

            transform.position = Vector3.Lerp(startCameraPoint.position, gamePlayCameraPoint.position, fractionOfDistance);
            transform.rotation = Quaternion.Slerp(startCameraPoint.rotation, gamePlayCameraPoint.rotation, rotationCovered);

            yield return null;
        }
    }

    public void ResetCamera()
    {
        distanceToMoveCamera = Vector3.Distance(startCameraPoint.position, gamePlayCameraPoint.position);
        transform.position = startCameraPoint.position;
        transform.rotation = startCameraPoint.rotation;

        StartCoroutine(WaitForRoundToStart());
    }

}
