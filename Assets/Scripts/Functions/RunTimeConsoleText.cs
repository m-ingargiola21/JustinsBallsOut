using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunTimeConsoleText : MonoBehaviour
{
    [SerializeField]
    GameObject gameObjectInput;

    RaycastVisualizer raycatVisualizer;

    public string consoleDebugString;
    TextMesh debugTextMesh;

	// Use this for initialization
	void Start ()
    {
        debugTextMesh = GetComponent<TextMesh>();
        raycatVisualizer = gameObjectInput.GetComponent<RaycastVisualizer>();
        consoleDebugString = raycatVisualizer.fRandom.ToString();
    }
	
	// Update is called once per frame
	void Update ()
    {
        //The degree the main camera is facing = Camera.main.transform.eulerAngles.y.ToString();

        //consoleDebugString = Camera.main.transform.eulerAngles.y.ToString();

        debugTextMesh.text = consoleDebugString;
	}
}