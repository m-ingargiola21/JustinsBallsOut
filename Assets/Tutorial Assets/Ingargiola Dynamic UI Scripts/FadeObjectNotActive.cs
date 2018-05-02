using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeObjectNotActive : MonoBehaviour
{
    private void OnEnable()
    {
        NRSRManager.ObjectFocused += FadeObject;
        NRSRManager.ObjectFocused += UnfadeObject;
    }

    private void OnDisable()
    {
        NRSRManager.ObjectFocused -= FadeObject;
        NRSRManager.ObjectFocused -= UnfadeObject;
    }

    void FadeObject()
    {
        if(gameObject.tag == "NRSRTools") { return; }

        //fade logic here
        Debug.Log(gameObject.name + "should fade");
    }

    void UnfadeObject()
    {
        if (gameObject.tag == "NRSRTools") { return; }

        //fade logic here
        Debug.Log(gameObject.name + "should unfade");
    }





    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}//end FadeObjectNotActive class
