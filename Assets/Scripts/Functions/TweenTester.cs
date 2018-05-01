using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class TweenTester : MonoBehaviour
{
    
    Transform[] tweenTransforms;
    Vector3[] path;
    LTSpline spline;
    TweenPathRotator tweenPathRotator;

    // Use this for initialization
    void Start ()
    {
        tweenPathRotator = GameObject.Find("TweenPathRotator").GetComponent<TweenPathRotator>();

        tweenPathRotator.LookAtSpawnedObject(transform.gameObject);
        
        tweenTransforms = tweenPathRotator.GetComponentsInChildren<Transform>();

        path = new Vector3[] { tweenTransforms[1].position, tweenTransforms[1].position,
            tweenTransforms[2].position, tweenTransforms[3].position,
            tweenTransforms[4].position, tweenTransforms[4].position };

        spline = new LTSpline(path);

        LeanTween.moveSpline(transform.gameObject, path, 5f).setEase(LeanTweenType.easeInOutQuad).setOrientToPath(false);
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    
}
