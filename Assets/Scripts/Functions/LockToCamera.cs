using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockToCamera : MonoBehaviour
{
    GameObject mainCamera;
    [SerializeField] float XAxisOffset;
    [SerializeField] float YAxisOffset;
    [SerializeField] float ZAxisOffset;
    [SerializeField] bool lockRotation;
    [SerializeField] bool lockRotationAll;


    void Start ()
    {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}


    void Update()
    {
        transform.position = new Vector3(mainCamera.transform.position.x + XAxisOffset,
            mainCamera.transform.position.y + YAxisOffset, mainCamera.transform.position.z + ZAxisOffset);

        if (lockRotation)
        {
            transform.rotation = new Quaternion(
                0, 
                mainCamera.transform.rotation.y,
                0, 
                mainCamera.transform.rotation.w);
        }
        else if(lockRotationAll)
        {
            transform.rotation = new Quaternion(
                mainCamera.transform.rotation.x, 
                mainCamera.transform.rotation.y,
                mainCamera.transform.rotation.z, 
                mainCamera.transform.rotation.w);
        }
    }
}