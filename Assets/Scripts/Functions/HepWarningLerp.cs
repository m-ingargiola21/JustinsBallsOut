using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class HepWarningLerp : MonoBehaviour
{

    string hepWarningName = "hep_warning";
    GameObject hepWarning;

    // Use this for initialization
    void Start()
    {

        hepWarning = GameObject.Find(hepWarningName);

        LeanTween.scale(hepWarning, new Vector3(.25f, .25f, .25f), 3f).setEase(LeanTweenType.easeInOutQuad).setLoopPingPong();

    }
    
    // Update is called once per frame
    void Update()
    {

    }

}