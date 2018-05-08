using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileControlsSwitch : MonoBehaviour
{
    public GameObject[][] playerControllers = new GameObject[4][];
    public GameObject[] playerControls1;
    public GameObject[] playerControls2;
    public GameObject[] playerControls3;
    public GameObject[] playerControls4;

    // Use this for initialization
    void Start ()
    {
        playerControllers[0] = playerControls1;
        playerControllers[1] = playerControls2;
        playerControllers[2] = playerControls3;
        playerControllers[3] = playerControls4;
    }
}
