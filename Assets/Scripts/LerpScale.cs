using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpScale : MonoBehaviour
{
    [SerializeField] float minLerp;
    [SerializeField] float maxLerp;
    [SerializeField] float timeLerp;

    float time;
    float perc;
    float scaleLerp;
	
	// Update is called once per frame
	void Update ()
    {
        time += Time.deltaTime;

        perc = Mathf.PingPong(time, timeLerp);
        scaleLerp = Mathf.Lerp(minLerp, maxLerp, perc);
	}
}
