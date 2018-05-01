using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    Color selectedColor;
    [SerializeField] GameObject playerBall;

	// Use this for initialization
	void Start ()
    {
        selectedColor = gameObject.GetComponent<Image>().color;
        //DefaultColor();
	}

    private void DefaultColor()
    {
       playerBall.GetComponent<MeshRenderer>().material.color = Color.blue;

    }

    // Update is called once per frame
    void Update ()
    {
        //PickColor();
	}

    

    public void PickColor()
    {
            playerBall.GetComponent<MeshRenderer>().material.color = selectedColor;
    }

}
