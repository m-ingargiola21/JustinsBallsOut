using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToObject : MonoBehaviour
{
    Transform objectToLockTo;
    public Transform ObjectToLockTo { get { return objectToLockTo; } set { objectToLockTo = value; } }
    [SerializeField]
    float XAxisOffset;
    [SerializeField]
    float YAxisOffset;
    [SerializeField]
    float ZAxisOffset;

    [SerializeField]
    bool lockRotation;

    
    void Update()
    {
        if(objectToLockTo != null)
        {
            transform.position = new Vector3(objectToLockTo.position.x + XAxisOffset,
            objectToLockTo.position.y + YAxisOffset, objectToLockTo.position.z + ZAxisOffset);
        }

        if (lockRotation)
        {
            transform.rotation = new Quaternion(objectToLockTo.rotation.x, objectToLockTo.rotation.y,
                objectToLockTo.rotation.z, objectToLockTo.rotation.w);
        }
    }
}
