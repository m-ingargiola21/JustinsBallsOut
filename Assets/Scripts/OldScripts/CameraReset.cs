using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    
    public void ResetCamera()
    {
        GameObject.Find("Main Camera").transform.position = new Vector3(0f, 0f, 0f);
    }

}
