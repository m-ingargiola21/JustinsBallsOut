using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlidersController : MonoBehaviour
{

    SliderDamager[] sliders;

    SliderDamager redSlider;
    SliderDamager blueSlider;
    SliderDamager greenSlider;
    
	// Use this for initialization
	void Start ()
    {

        sliders = GameObject.Find("Canvas").GetComponentsInChildren<SliderDamager>();

        blueSlider = sliders[0];
        redSlider = sliders[1];
        greenSlider = sliders[2];

        foreach (SliderDamager s in sliders)
        {

            s.SetValue("Normal");

        }

	}
	
	// Update is called once per frame
	void Update ()
    {
		


	}



}
