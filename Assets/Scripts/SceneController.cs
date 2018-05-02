using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class SceneController : MonoBehaviour
{
    public GameObject trackedPlanePrefab;
    public Camera firstPersonCamera;
    public ScoreboardController scoreboard;
    public ARTestSnakeController snakeController;

    void Start ()
    {
        QuitOnConnectionErrors();
    }
	
	void Update ()
    {
        Debug.Log("Debug test");
        // The session status must be Tracking in order to access the Frame.
        if (Session.Status != SessionStatus.Tracking)
        {
            const int lostTrackingSleepTimeout = 15;
            Screen.sleepTimeout = lostTrackingSleepTimeout;
            return;
        }
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ProcessNewPlanes();

        ProcessTouches();

        scoreboard.SetScore(snakeController.GetLength());
    }

    void QuitOnConnectionErrors()
    {
        // Do not update if ARCore is not tracking.
        if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
        {
            StartCoroutine(CodelabUtils.ToastAndExit(
                  "Camera permission is needed to run this application.", 5));
        }
        else if (Session.Status.IsError())
        {
            // This covers a variety of errors.  See reference for details
            // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
            StartCoroutine(CodelabUtils.ToastAndExit(
              "ARCore encountered a problem connecting. Please restart the app.", 5));
        }
    }

    void ProcessNewPlanes()
    {
        List<TrackedPlane> planes = new List<TrackedPlane>();
        Session.GetTrackables(planes, TrackableQueryFilter.New);

        for (int i = 0; i < planes.Count; i++)
        {
            // Instantiate a plane visualization prefab and set it to track the new plane.
            // The transform is set to the origin with an identity rotation since the mesh
            // for our prefab is updated in Unity World coordinates.
            GameObject planeObject = Instantiate(trackedPlanePrefab, Vector3.zero,
                    Quaternion.identity, transform);
            planeObject.GetComponent<TrackedPlaneController>().SetTrackedPlane(planes[i]);
        }
    }

    void ProcessTouches()
    {
        Touch touch;
        if (Input.touchCount != 1 ||
            (touch = Input.GetTouch(0)).phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;
        TrackableHitFlags raycastFilter =
            TrackableHitFlags.PlaneWithinBounds |
            TrackableHitFlags.PlaneWithinPolygon;

        Ray ray = Camera.main.ScreenPointToRay(new Vector2(touch.position.x, touch.position.y));
        RaycastHit raycastHit;

        if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
        {
            Debug.Log("Touched a plane.");
            SetSelectedPlane(hit.Trackable as TrackedPlane);
        }
        else if(Physics.Raycast(ray, out raycastHit))
        {
            Debug.Log("Touch input hit something.");
            if (raycastHit.transform.tag == "UI")
            {
                EventManager.CallUITouch(raycastHit.transform);
            }
            else if (raycastHit.transform.tag == "Food")
            {
                EventManager.CallFoodTouch(raycastHit.transform);
            }
            else if (raycastHit.transform.tag == "Predators")
            {
                EventManager.CallSnakeTouch(raycastHit.transform);
            }
        }
    }

    void SetSelectedPlane(TrackedPlane selectedPlane)
    {
        Debug.Log("Selected plane centered at " + selectedPlane.CenterPose.position);

        // Add to the end of SetSelectedPlane.
        scoreboard.SetSelectedPlane(selectedPlane);

        // Add to SetSelectedPlane()
        snakeController.SetPlane(selectedPlane);

        // Add to the bottom of SetSelectedPlane() 
        GetComponent<ARFoodController>().SetSelectedPlane(selectedPlane);
    }
}
