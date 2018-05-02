//using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundingBox : MonoBehaviour//, IFocusable
{
    //Material currentMat;
    Material BoundingBoxMat;        //BoundingBox Material
    GameObject SRSBoundingBox;      // Our BoundingBox
    Bounds SRSBounds;               // What will be Bounds collection
    bool isActive;                  //is our BoundingBox Active
    public bool BoundingBoxCreated;     //Has our BoundingBox been created?
    public bool isRootObject;

    // Use this for initialization
    void Start ()
    {
        //BoundingBoxMat = NRSRManager.Instance.BoundingBoxMat;

	}//end Start()

    void Update()
    {
        //if the BoundingBoxCreated bool is false and it is the root object,(for a few edge cases where non root objects found themselves with
        //BoundingBox class attached) CreateBoundingBox and set the bool to true
        if (!BoundingBoxCreated && isRootObject)
        {
            CreateBoundingBox();
            BoundingBoxCreated = true;
            return;
        }

        //now test where our bounding box is null, if not test whether it is active or not. Set true or false accordingly.
        if (SRSBoundingBox != null)
        {
            if (!isActive)
            {
                SRSBoundingBox.SetActive(false);
                return;
            }
            if (isActive)
            {
                SRSBoundingBox.SetActive(true);
            }
        }
    }

    void CreateBoundingBox()
    {
        //lets creat a copy of the object that we are making a bounding box of.
        SRSBoundingBox = Instantiate(gameObject);
        // name it bounding box
        SRSBoundingBox.name = "BoundingBox";

        //the object we copied should be have the boundingbox class attached, so if it doesn't, destroy the object.
        if (SRSBoundingBox.GetComponent<BoundingBox>() == null)
        {
            Destroy(SRSBoundingBox);
            return;
        }
        else
        {
            //the bounding box class should only be attached to the root object, otherwise it can create infinite loops
            // so once the copy is instantiated check and see if we have a bounding box class and if we do destroy it.
            Destroy(SRSBoundingBox.GetComponent<BoundingBox>());
            // Make sure we tag it correctly
            SRSBoundingBox.tag = "NRSRTools";
            //set the scale .1f up from its normal scale.
            SRSBoundingBox.transform.localScale *= 1.1f;

            //make sure it is parented to the object we copied.
            SRSBoundingBox.transform.parent = gameObject.transform;

            //find all of the children attached to our bounding box object.
            List<Transform> children = new List<Transform>(SRSBoundingBox.GetComponentsInChildren<Transform>());
            Debug.Log("Children");

            //loop through all of the children and if there have a meshRenderer, use our Bounding Box Material on the objects
            //then make sure they are all parented to the Bounding Box Object.
            foreach (Transform child in children)
            {
                child.tag = "NRSRTools";
                if (child.GetComponent<MeshRenderer>() != null)
                {
                    Debug.Log("Yes! Chef!");
                    child.GetComponent<MeshRenderer>().material = BoundingBoxMat;
                    child.transform.parent = SRSBoundingBox.transform;
                }
            }
        }

        //Time to add the Endpoint Handles.

        //check and see if the object has children - this will work find for objects that do not.
        List<MeshFilter> childrenBounds = new List<MeshFilter>(SRSBoundingBox.GetComponentsInChildren<MeshFilter>());

        //loop through each child, check if the meshfilter exists and then add its bounds to the SRSBounds.
        foreach (MeshFilter meshRen in childrenBounds)
        {
            if (meshRen.GetComponent<MeshFilter>() != null)
            {
                Debug.Log(meshRen.gameObject.name);
                SRSBounds.Encapsulate(meshRen.GetComponent<MeshFilter>().mesh.bounds);
            }
        }


        //8.Create 8 points around the object
        Vector3 SRSPoint0 = SRSBounds.min * gameObject.transform.localScale.x * 1.1f;
        Vector3 SRSPoint1 = SRSBounds.max * gameObject.transform.localScale.z * 1.1f;
        Vector3 SRSPoint2 = new Vector3(SRSPoint0.x, SRSPoint0.y, SRSPoint1.z);
        Vector3 SRSPoint3 = new Vector3(SRSPoint0.x, SRSPoint1.y, SRSPoint0.z);
        Vector3 SRSPoint4 = new Vector3(SRSPoint1.x, SRSPoint0.y, SRSPoint0.z);
        Vector3 SRSPoint5 = new Vector3(SRSPoint0.x, SRSPoint1.y, SRSPoint1.z);
        Vector3 SRSPoint6 = new Vector3(SRSPoint1.x, SRSPoint0.y, SRSPoint1.z);
        Vector3 SRSPoint7 = new Vector3(SRSPoint1.x, SRSPoint1.y, SRSPoint0.z);

        //9.Create the scaling handles.
        CreateEndPoints(SRSPoint0 + transform.position);
        CreateEndPoints(SRSPoint1 + transform.position);
        CreateEndPoints(SRSPoint2 + transform.position);
        CreateEndPoints(SRSPoint3 + transform.position);
        CreateEndPoints(SRSPoint4 + transform.position);
        CreateEndPoints(SRSPoint5 + transform.position);
        CreateEndPoints(SRSPoint6 + transform.position);
        CreateEndPoints(SRSPoint7 + transform.position);

    }


    void CreateEndPoints(Vector3 position)
    {
        //Create Handle Object
        GameObject CornerHandle = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        CornerHandle.name = "Handle";
        CornerHandle.tag = "NRSRTools";

        //Set The Scale
        CornerHandle.transform.localScale = new Vector3(0.15f, 0.15f, 0.15f);

        //Set The Position
        CornerHandle.transform.position = position;

        //Set The Parent
        CornerHandle.transform.parent = SRSBoundingBox.transform;

        //Set The Material
        CornerHandle.GetComponent<Renderer>().material = BoundingBoxMat;

    }

    public void OnFocusEnter()
    {
        //if the BoundingBox has not been created return
        if (!BoundingBoxCreated)
        {
            return;
        }
        //if it is currently active return
        if (isActive)
        {
            return;
        }
        NRSRManager.SendFocusedObjectToManager(gameObject);
        //if those conditions do not apply - set isActive to true
        isActive = true;
    }

    public void OnFocusExit()
    {
        //if the BoundingBox has not been created return
        if (!BoundingBoxCreated)
        {
            return;
        }
        //if it is not currently active return
        if (!isActive)
        {
            return;
        }
         
        if(NRSRManager.holdSelectedObject_UsingTransformTool) { return; }

        if (NRSRManager.holdSelectedObject_LookingAtTransformTool) { return; }

        //if those conditions do not apply - set isActive to false
        NRSRManager.ClearFocusedObjectFromManager();
        isActive = false;
    }


}//end BoundingBox Class

/* // Update is called once per frame
	void Update ()
    {
        //Do I need Update???
		
	}//end Update()

    public void OnFocusEnter()
    {
        ObjectFocused();

    }//end OnFocusEnter()

    public void OnFocusExit()
    {
        ObjectUnfocused();

    }//end OnFocusExit()

    void ObjectFocused()
    {
        Renderer rend = GetComponent<Renderer>();
            currentMat = rend.material;
            rend.material = BoundingBoxMat;

    }//end ObjectFocused()

void ObjectUnfocused()
    {
        Renderer rend = GetComponent<Renderer>();
            rend.material = currentMat;

    }//end ObjectUnfocused()


    Let's do a play-by-play with the code here.

Instantiate (gameObject) is actually creating an exact copy of the object BoundingBox is attached to — and anything attached to it.
We then name the object "BoundingBox".
If our newly created object does not have BoundingBox attached to it, destroy it. It should. We should only ever be copying the root object with BoundingBox attached.
If it does have BoundingBox attached, destroy the BoundingBox class and begin adding the things we need. While we the object the class on that object can create an infinite loop, which as we said, is bad.
Add our "NRSRTools" to the new object.
Make our scale 1.1f of the original object.
Set the object's parent to be our original object.
Now we will create a list of transforms called "children" using GetComponentsInChildren<Transform>() to fill that list.

As continue the procedure, we will put our first material on each of the objects in the list.

We loop through the children list and change their tag to NRSRTools so the system will ignore them.
Then we determine if they have a MeshRenderer attached. We don't need any crashes, and not all objects do.
Then we have our children letting Chef know they are present.
Then we apply the material and reparent them to the recently cloned object.
After adding our material to all the objects, it is time to actually create our bounds. This is done using the Bounds.Encapsulate() method.


Create a List<> of all the children in the hierarchy that contain a MeshFilter.
Loop through the list to make sure it actually does have a MeshFilter.
Using the Encapsulate() method, add this current child's bounds to our main bounds object.




 */
