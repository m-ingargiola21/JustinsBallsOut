using System.Collections;
using System.Collections.Generic;
//using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class MoveTool : MonoBehaviour
{

    public Transform HostTransform;

    [Range(0.01f, 1.0f)]
    public float PositionLerpSpeed = 0.2f;


    [Range(0.01f, 1.0f)]
    public float RotationLerpSpeed = 0.2f;

    public float DistanceScale = 8f;

    public bool IsDraggingEnabled = true;

    private bool isDragging;
    private bool isGazed;

    private Vector3 manipulationEventData;
    private Vector3 manipulationDelta;

    private Camera mainCamera;
    //private IInputSource currentInputSource;
    private uint currentInputSourceId;

    void Start()
    {
        if (HostTransform == null)
        {
            //This is temporary so we dont get a null exception.
            HostTransform = transform;
        }

        mainCamera = Camera.main;
    }

    void Update()
    {
        Quaternion currentRot = HostTransform.transform.rotation;

        if (NRSRManager.FocusedObject != null)
        {
            HostTransform = NRSRManager.FocusedObject.transform;
        }
        else
        {
            return;
        }
        if (IsDraggingEnabled && isDragging)
        {
            HostTransform.position = Vector3.Lerp(HostTransform.position, HostTransform.position + manipulationDelta, PositionLerpSpeed);
        }
    }

    public void StartDragging()
    {
        if (!IsDraggingEnabled)
        {
            return;
        }

        if (isDragging)
        {
            return;
        }
        NRSRManager.holdSelectedObject_UsingTransformTool = true;
    }

    public void StopDragging()
    {
        if (!isDragging)
        {
            NRSRManager.holdSelectedObject_UsingTransformTool = false;
            return;
        }

        //InputManager.Instance.PopModalInputHandler();
        isDragging = false;
        //currentInputSource = null;
    }

    public void OnFocusEnter()
    {
        if (!IsDraggingEnabled)
        {
            return;
        }
        if (isGazed)
        {
            return;
        }
        isGazed = true;
    }

    public void OnFocusExit()
    {
        if (!IsDraggingEnabled)
        {
            return;
        }
        if (!isGazed)
        {
            return;
        }
        isGazed = false;
    }

    //public void OnInputDown(InputEventData eventData)
    //{

    //    if (isDragging)
    //    {
    //        return;
    //    }

    //    if (!eventData.InputSource.SupportsInputInfo(eventData.SourceId, SupportedInputInfo.Position))
    //    {
    //        return;
    //    }

    //    if (!IsDraggingEnabled)
    //    {
    //        return;
    //    }
    //    InputManager.Instance.PushModalInputHandler(gameObject);

    //    isDragging = true;
    //    currentInputSource = eventData.InputSource;
    //    currentInputSourceId = eventData.SourceId;
    //    StartDragging();
    //}

    //public void OnInputUp(InputEventData eventData)
    //{
    //    Debug.Log("OnInputUp");
    //    if (currentInputSource != null &&
    //           eventData.SourceId == currentInputSourceId)
    //    {
    //        StopDragging();
    //    }
    //}

    //public void OnSourceDetected(SourceStateEventData eventData)
    //{

    //}

    //public void OnSourceLost(SourceStateEventData eventData)
    //{
    //    if (currentInputSource != null && eventData.SourceId == currentInputSourceId)
    //    {
    //        StopDragging();
    //    }
    //}

    //public void OnManipulationStarted(ManipulationEventData eventData)
    //{
    //    Debug.LogFormat("OnManipulationStarted\r\nSource: {0}  SourceId: {1}\r\nCumulativeDelta: {2} {3} {4}",
    //           eventData.InputSource,
    //           eventData.SourceId,
    //           eventData.CumulativeDelta.x,
    //           eventData.CumulativeDelta.y,
    //           eventData.CumulativeDelta.z);
    //    manipulationEventData = eventData.CumulativeDelta;
    //}

    //public void OnManipulationUpdated(ManipulationEventData eventData)
    //{
    //    Debug.LogFormat("OnManipulationUpdated\r\nSource: {0}  SourceId: {1}\r\nCumulativeDelta: {2} {3} {4}",
    //               eventData.InputSource,
    //               eventData.SourceId,
    //               eventData.CumulativeDelta.x,
    //               eventData.CumulativeDelta.y,
    //               eventData.CumulativeDelta.z);

    //    Vector3 delta = eventData.CumulativeDelta - manipulationEventData;
    //    manipulationDelta = delta * DistanceScale;
    //    manipulationEventData = eventData.CumulativeDelta;


    //}

    //public void OnManipulationCompleted(ManipulationEventData eventData)
    //{
    //    manipulationEventData = Vector3.zero;
    //    manipulationDelta = Vector3.zero;
    //}

    //public void OnManipulationCanceled(ManipulationEventData eventData)
    //{

    //}

}
