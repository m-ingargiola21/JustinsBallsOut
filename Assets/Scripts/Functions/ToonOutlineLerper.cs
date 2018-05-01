using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class ToonOutlineLerper : MonoBehaviour
{
    [SerializeField] Material redOutlineMat;
    [SerializeField] Material blueOutlineMat;
    [SerializeField] Material greenOutlineMat;
    [SerializeField] Material ultraOutlineMat;
    [SerializeField] float lerpTime = 1f;
    float currentLerpTime;
    Color black = Color.black;
    Color yellow = Color.yellow;
    Color lerpedColor;
    bool isIncreasing;

    void Start ()
    {
        isIncreasing = true;
        StartCoroutine(lerpColor());
	}
	
    IEnumerator lerpColor()
    {
        while(true)
        {
            if(isIncreasing)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime > lerpTime)
                {
                    currentLerpTime = lerpTime;
                    isIncreasing = false;
                }
            }
            else
            {
                currentLerpTime -= Time.deltaTime;
                if (currentLerpTime < 0f)
                {
                    currentLerpTime = 0f;
                    isIncreasing = true;
                }
            }
            

            float t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            lerpedColor = Color.Lerp(black, yellow, t);

            redOutlineMat.SetColor("_OutlineColor", lerpedColor);
            blueOutlineMat.SetColor("_OutlineColor", lerpedColor);
            greenOutlineMat.SetColor("_OutlineColor", lerpedColor);
            ultraOutlineMat.SetColor("_OutlineColor", lerpedColor);

            yield return null;
        }
    }
}
