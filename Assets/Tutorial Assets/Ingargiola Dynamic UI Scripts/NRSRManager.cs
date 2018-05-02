using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
//using HoloToolkit.Unity;

public class NRSRManager //: Singleton<NRSRManager>
{
    public Renderer[] ObjectsInScene;
    public List<GameObject> FilterObjectsInScene = new List<GameObject>();
    //Used to trigger next object update
    public int TotalNumberOfObjects = 0;
    public int PreviousFrameObjectCount = 0;

    public static GameObject FocusedObject;

    public delegate void OnObjectFocused();
    public static event OnObjectFocused ObjectFocused;
    public static event OnObjectFocused ObjectUnFocused;

    //just in case we need - these numbers added together , should always = total number of objects
    public int numberOfVisibleObjects;
    public int numberOfFilteredObjects;

    public Material BoundingBoxMat;

    public RaycastHit hitInfo;
    public static bool holdSelectedObject_LookingAtTransformTool;
    public static bool holdSelectedObject_UsingTransformTool;
    LayerMask layerMask;
    public static Vector3 menuPosition;


    // Use this for initialization
    void Start()
    {
        layerMask = 1 << 23;
        layerMask = ~layerMask;
    }

    // Update is called once per frame
    private void Update()
    {
        if (holdSelectedObject_UsingTransformTool)
        {
            return;
        }

        RayCastToHoldFocusedObject();
        menuPosition = hitInfo.point;

        if (holdSelectedObject_LookingAtTransformTool)
        {
            if (ObjectFocused != null)
            {
                ObjectFocused();
            }
            return;
        }
        else
        {

            FocusedObject = null;
            if (ObjectUnFocused != null)
            {

                ObjectUnFocused();
            }
        }
    }//end Update()
    private void FixedUpdate()
    {
        FindObjectsInScene(); //Likely expensive to do every frame, dynamic systems tend to come at a cost.

        TotalNumberOfObjects = ObjectsInScene.Length;

        if(TotalNumberOfObjects != PreviousFrameObjectCount)
        {
            FilterUnneededObjects();
            numberOfVisibleObjects = FilterObjectsInScene.Count;

            foreach (GameObject go in FilterObjectsInScene)
            {
                //checks to see if each root object in FilteredObjects has the BoundingBox class attached
                //if null adds the BoundingBox class and sets RootObject bool to true
                if(go.transform.root.gameObject.GetComponent<BoundingBox>() == null)
                {
                    BoundingBox box = go.transform.root.gameObject.AddComponent<BoundingBox>();
                    go.transform.root.gameObject.AddComponent<FadeObjectNotActive>();
                    box.isRootObject = true;

                }//end if
            }//end foreach
        }//end if

        PreviousFrameObjectCount = ObjectsInScene.Length;

    }//end FixedUpdate()


    void FindObjectsInScene()
    {
        ObjectsInScene = null;
       // ObjectsInScene = FindObjectsOfType<Renderer>();

    }//end FindObjectInScene()

    void FilterUnneededObjects()
    {
        FilterObjectsInScene.Clear();
        numberOfFilteredObjects = 0;

        for (int i = 0; i < ObjectsInScene.Length; i++)
        {
            if (ObjectsInScene[i].gameObject.tag != "NRSRTools")//TODO make tag viewable to Editor
            {
                FilterObjectsInScene.Add(ObjectsInScene[i].gameObject);
            }
            else
            {
                numberOfFilteredObjects++;
            }
        }
    }//end FilterUnneededObjects()

    public static void SendFocusedObjectToManager(GameObject go)
    {
        FocusedObject = go;
        Debug.Log(go.name + " to NRSRManager");

    }//end SendFocusedObjectToManager
    public static void ClearFocusedObjectFromManager()
    {

        FocusedObject = null;

    }//end ClearFocusedObjectFromManager()

    public void RayCastToHoldFocusedObject()
    {
        if (Physics.Raycast(Camera.main.transform.position,
                            Camera.main.transform.forward,
                            out hitInfo,
                            Mathf.Infinity,
                            layerMask))
        {
            Debug.Log(hitInfo.transform.root.name);

            if (hitInfo.transform == null)
            {
                Debug.Log("hitInfo Transform null");
                NRSRManager.holdSelectedObject_LookingAtTransformTool = false;
                return;
            }

            if (FocusedObject != null)
            {
                if (FocusedObject.transform.root.name == hitInfo.transform.root.name)
                {
                    NRSRManager.holdSelectedObject_LookingAtTransformTool = true;
                }
                else
                {
                    NRSRManager.holdSelectedObject_LookingAtTransformTool = false;
                }
            }

        }
        else
        {
            NRSRManager.holdSelectedObject_LookingAtTransformTool = false;
        }
    }//end RayCastToHoldFocusedObject()


}//end class NRSRManager

/*
 Update() method:

If our SelectedObject is using the transform tool immediately leave.
Run our RaycastToHoldFocusedObject() which we will dig into shortly.
next we set our menuPosition to the hitInfo.point that we create in the RaycastToHoldFocusedObject()
Finally, in the last conditional (if/else) block we replaced, the FocusedObject logic with a test based 
on whether or not we are looking at the transform tool. The bool name would be more accurate to call it 
holdSelectedObjectLookingAtTransformToolOrObject but it was getting pretty ridiculous as it was.


 RayCastToHoldFocusedObject() method.

Raycasts are generally done in if statements and return true or false. This is exactly we have here. 
We are using our camera's position and forward vector as for the ray itself.
If the ray doesn't hit anything we set our LookingAt bool to false and eject.
If we are looking at something and we have a FocusedObject, 
we test whether the name of the FocusedObject and the name of the object that is being hit by the ray are the same.
If so set our LookingAt bool to true.
If they are not the same set LookingAt to false.
Finally if the ray did not hit anything at all set LookingAt to false.


     */
