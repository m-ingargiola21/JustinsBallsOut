using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceCamera : MonoBehaviour {

	// faces the names of the players toward the camera
	void Update () {
        transform.LookAt(Camera.main.transform.position);
        transform.Rotate(new Vector3(0, 180, 0));
	}
}
