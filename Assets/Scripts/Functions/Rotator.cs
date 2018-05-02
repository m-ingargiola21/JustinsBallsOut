using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    public int RotateX = 15;
    public int RotateY = 30;
    public int RotateZ = 45;
    public int RotatorSpeedAmplifier = 1;

    void Update()
    {
        transform.Rotate(new Vector3(RotateX, RotateY, RotateZ) * Time.deltaTime * RotatorSpeedAmplifier);
    }

}

