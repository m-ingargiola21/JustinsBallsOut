using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine;

public class Drive : MonoBehaviour {
    // For the driving around of the car.  Nothing more than network testing, really.
    public float DriveSpeed = 10f;
    public float RotationSpeed = 100f;

	// Update is called once per frame
	void Update () {
        float translation = CrossPlatformInputManager.GetAxis("Vertical") * DriveSpeed;
        float rotation = CrossPlatformInputManager.GetAxis("Horizontal") * RotationSpeed;

        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;
        transform.Translate(0, 0, translation);
        transform.Rotate(0, rotation, 0);
    }
}
