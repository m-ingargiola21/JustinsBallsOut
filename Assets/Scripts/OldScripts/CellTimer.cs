using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class determines how fast and if a cell should spawn an object, JUSTIN DOUBLE CHECK THIS
/// </summary>
public class CellTimer : MonoBehaviour
{
    [SerializeField] float countDownTimer = 5f;
    [SerializeField] float timerMinimum = 1.5f;
    [SerializeField] float timerTickDownAmount = .1f;
    float countDownResetTime;
    int tickDownMultiplier = 1;
    bool isActive;
    bool shouldSpawn;

    bool isRunning;
    public bool IsActive
    {
        get { return isActive; }
    }

    public float CountDownTimer
    {
        get { return countDownTimer; }
    }

    public bool ShouldSpawn
    {
        get { return shouldSpawn; }
    }

    RunTimeConsoleText rtcText;

    // Use this for initialization
    void Start()
    {
        countDownResetTime = countDownTimer;
        isRunning = true;

        StartCoroutine(TimerCoroutine());
    }

    IEnumerator TimerCoroutine()
    {
        while (isRunning)
        {
            if (isActive)
            {
                countDownTimer -= Time.deltaTime;

                if (countDownTimer <= .02f)
                {
                    EventManagerOld.CallSpawnEvents();

                    if ((countDownResetTime - timerTickDownAmount * tickDownMultiplier) > timerMinimum)
                    {
                        countDownTimer = countDownResetTime - timerTickDownAmount * tickDownMultiplier;

                        tickDownMultiplier++;
                    }
                    else
                        countDownTimer = timerMinimum;
                }
            }

            yield return null;
        }
    }

    //private void LateUpdate()
    //{

    //    if (isActive)
    //    {
    //        shouldSpawn = false;

    //        countDownTimer -= Time.deltaTime;

    //        if (countDownTimer <= .02f)
    //        {
    //            shouldSpawn = true;

    //            if ((countDownResetTime - timerTickDownAmount * tickDownMultiplier) > timerMinimum)
    //            {
    //                countDownTimer = countDownResetTime - timerTickDownAmount * tickDownMultiplier;

    //                tickDownMultiplier++;
    //            }
    //            else
    //                countDownTimer = timerMinimum;
    //        }
    //    }
    //}

    public void StartTimer()
    {
        isActive = true;
    }

    public void StopTimer()
    {
        isActive = false;
    }

    public void ResetTimer()
    {
        countDownTimer = countDownResetTime;
        isActive = false;
        tickDownMultiplier = 1;
    }
}