using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenPathRotator : MonoBehaviour
{

    Vector3 objectToLookAtModified;

    public void LookAtSpawnedObject(GameObject objectToLookAt)
    {

        objectToLookAtModified = new Vector3(objectToLookAt.transform.position.x,
            0, objectToLookAt.transform.position.z);

        transform.LookAt(objectToLookAtModified);

    }

}
