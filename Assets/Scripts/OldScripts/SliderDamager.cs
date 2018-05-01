using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DentedPixel;

public class SliderDamager : MonoBehaviour
{
    [SerializeField]
    Image warningIndicator;
    [SerializeField]
    float warningAmount = .25f;

    Slider slider;
    Image fill;
    Color setColor;
    Color black;
    Color warningImageStartColor;

    float lerpTime = 1.5f;

    // Use this for initialization
    void Start ()
    {

        fill = transform.GetComponentInChildren<Image>();

        setColor = fill.color;
        black = Color.black;
        warningImageStartColor = warningIndicator.color;

	}

    private void Update()
    {

        fill.color = Color.Lerp(black, setColor, slider.value);

        if (slider.value <= warningAmount)
        {

            if (!LeanTween.isTweening())
                LeanTween.value(transform.gameObject, WarningIndicatorTween, warningImageStartColor, Color.red, lerpTime).setDelay(0f).setEaseInOutQuad().setLoopPingPong();
            //StartCoroutine("LerpWarningImage");

        }
        else
        {

            LeanTween.cancel(transform.gameObject);
            //StopAllCoroutines();
            warningIndicator.color = warningImageStartColor;

        }
            
    }

    void WarningIndicatorTween(Color tweenColor)
    {
        warningIndicator.color = tweenColor;
    }

    IEnumerator LerpWarningImage()
    {

        //float lerpTime = 1.5f;
        float tweenFloat;
        //float currentLerpTime = 0.0f;
        //float t = 0.0f;

        while (slider.value <= warningAmount)
        {

            LeanTween.value(transform.gameObject, WarningIndicatorTween, warningImageStartColor, Color.red, lerpTime).setEaseInOutQuad().setLoopPingPong();
            
            //if (warningIndicator.color == warningImageStartColor)
            //{

            //    currentLerpTime = 0.0f;

            //    while (warningIndicator.color != Color.red)
            //    {

            //        currentLerpTime += Time.deltaTime;

            //        if (currentLerpTime > lerpTime)
            //        {
            //            currentLerpTime = lerpTime;
            //        }

            //        t = currentLerpTime / lerpTime;
            //        t = t * t * t * (t * (6f * t - 15f) + 10f);

            //        warningIndicator.color = Color.Lerp(warningImageStartColor, Color.red, t);

            //        yield return null;

            //    }
                
            //}
            //else if (warningIndicator.color == Color.red)
            //{

            //    currentLerpTime = 0.0f;

            //    while (warningIndicator.color != warningImageStartColor)
            //    {

            //        currentLerpTime += Time.deltaTime;

            //        if (currentLerpTime > lerpTime)
            //        {
            //            currentLerpTime = lerpTime;
            //        }

            //        t = currentLerpTime / lerpTime;
            //        t = t * t * t * (t * (6f * t - 15f) + 10f);

            //        warningIndicator.color = Color.Lerp(Color.red, warningImageStartColor, t);

            //        yield return null;

            //    }
                
            //}

            yield return null;

        }
        
    }

    public void TakeDamage(float damageAmount)
    {

        if (slider.value - damageAmount > 0.0f)
            slider.value -= damageAmount;
        else
            slider.value = 0.0f;

    }

    public void SetValue(string difficulty)
    {

        slider = transform.GetComponent<Slider>();

        slider.minValue = 0.0f;
        slider.maxValue = 1.0f;

        switch (difficulty)
        {
            case "Normal":
                
                slider.value = Random.Range(.4f, .6f);

                break;
            case "Hard":

                slider.value = Random.Range(.25f, .5f);

                break;
            default:
                break;
        }

    }

}
