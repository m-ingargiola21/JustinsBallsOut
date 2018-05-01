using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    [SerializeField] Color selectedColor;
    [SerializeField] GameObject playerBall;

	// Use this for initialization
	void Start ()
    {
        selectedColor = gameObject.GetComponent<Image>().color;
        DefaultColor();
	}

    private void DefaultColor()
    {
       playerBall.GetComponent<MeshRenderer>().material.color = Color.blue;

    }

    // Update is called once per frame
    void Update ()
    {
        PickColor();
	}

    void PickColor()
    {
        //input: click and tap
        //select color from options and get the color of that option
        if (Input.GetMouseButton(0))//left click
        {
            //set the player's ball to the selecetd color
            playerBall.GetComponent<MeshRenderer>().material.color = selectedColor;

        }
    }

}
