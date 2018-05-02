using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] TextMesh gameOverText;
    [SerializeField] Color orange;
    float colorChangeDelay = .5f;
    WaitForSeconds delay;
    float lerpTime = 3f;
    float currentLerpTime = 0;
    float t = 0;
    int lerppedFontSize = 0;
    
	void Start ()
    {
        delay = new WaitForSeconds(colorChangeDelay);
        StartCoroutine(GameOverTextVisual());
	}

    IEnumerator GameOverTextVisual()
    {
        bool isIncreasing = false;
        while (currentLerpTime < lerpTime)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            t = currentLerpTime / lerpTime;
            t = t * t * t * (t * (6f * t - 15f) + 10f);

            lerppedFontSize = Mathf.RoundToInt(Mathf.Lerp(0, 60, t));

            gameOverText.fontSize = lerppedFontSize;

            yield return null;
        }

        StartCoroutine(GameOverTextColor());

        while (true)
        {
            if(!isIncreasing)
            {
                currentLerpTime -= Time.deltaTime;
                t = currentLerpTime / lerpTime;

                t = t * t * t * (t * (6f * t - 15f) + 10f);

                lerppedFontSize = Mathf.RoundToInt(Mathf.Lerp(30, 60, t));

                gameOverText.fontSize = lerppedFontSize;

                if (lerppedFontSize <= 50)
                    isIncreasing = true;
            }
            else
            {
                currentLerpTime += Time.deltaTime;
                t = currentLerpTime / lerpTime;

                if (currentLerpTime > lerpTime)
                {
                    currentLerpTime = lerpTime;
                }

                t = t * t * t * (t * (6f * t - 15f) + 10f);

                lerppedFontSize = Mathf.RoundToInt(Mathf.Lerp(30, 60, t));

                gameOverText.fontSize = lerppedFontSize;

                if (lerppedFontSize == 60)
                    isIncreasing = false;
            }

            yield return null;
        }
    }

    IEnumerator GameOverTextColor()
    {
        while(true)
        {
            gameOverText.color = Color.yellow;

            yield return delay;

            gameOverText.color = orange;

            yield return delay;

            gameOverText.color = Color.red;

            yield return delay;
        }
    }
	
}
