using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformMovement : MonoBehaviour
{

    Bounds toolBounds;
    GameObject cursor;
    Vector3 menuPosition;
    bool initialSetupComplete = true;

    void OnEnable()
    {


        NRSRManager.ObjectFocused += Enabled_GetPosition;
        NRSRManager.ObjectUnFocused += Disabled_Reset;

        toolBounds = GetBoundsForAllChildren(gameObject);
        cursor = GameObject.Find("Cursor");
    }

    private void OnDisable()
    {
        NRSRManager.ObjectFocused -= Enabled_GetPosition;
        NRSRManager.ObjectUnFocused -= Disabled_Reset;
    }

    void Disabled_Reset()
    {

        initialSetupComplete = true;
    }

    void Enabled_GetPosition()
    {
        if (initialSetupComplete)
        {
            transform.position = cursor.transform.position;
            transform.rotation = cursor.transform.rotation * Quaternion.Euler(0, 0, 180);
            initialSetupComplete = false;

        }
    }

    void Update()
    {
        //get the updated hitinfo position every frame
        menuPosition = NRSRManager.menuPosition;

    
        if (transform.localPosition.x - menuPosition.x > toolBounds.extents.x / 3 ||
            transform.localPosition.x - menuPosition.x < -toolBounds.extents.x / 3 ||
            transform.localPosition.y - menuPosition.y > toolBounds.extents.y / 3 ||
            transform.localPosition.y - menuPosition.y < -toolBounds.extents.y / 3)
        {
            transform.position = Vector3.Lerp(transform.position, cursor.transform.position, 0.02f);
        }

        transform.rotation = Quaternion.Lerp(transform.rotation,
                                             cursor.transform.rotation * Quaternion.Euler(0, 0, 180),
                                             1);

        transform.position = new Vector3(transform.position.x,
                                         transform.position.y,
                                         cursor.transform.position.z - 0.1f);

    }

    public Bounds GetBoundsForAllChildren(GameObject findMyBounds)
    {
        Bounds result = new Bounds(Vector3.zero, Vector3.zero);

        foreach (Collider coll in findMyBounds.GetComponentsInChildren<Collider>())
        {
            if (result.extents == Vector3.zero)
            {
                result = coll.bounds;
            }
            else
            {
                result.Encapsulate(coll.bounds);
            }
        }

        return result;
    }
}//end TransformMovement class
