using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// What is the difference between CELLTIMER and Timer Classes??
/// There isn't one, except that when I originally used the Timer class on the Cell it didn't work.  So I thought
/// I would have to change it to get it to work and made CellTimer, this is also why the CellTimer has
/// the RunTimeConsoleText in it, because I was using that to debug a problem.  In the end I think it was an issue
/// with me not calling the function that started the Time in the SpawnManager and once I changed it
/// it started working fine so we could eliminate it and just use the Time if you want... Unless we are going to change
/// the CellTimer in the future to something different, then we can keep it and make them differnt.  Doesn't matter to me.
/// </summary>
public class Timerold : MonoBehaviour
{
    [SerializeField] float countDownTimer = 5f;

    public float CountDownTimer
    {
        get { return countDownTimer; }
    }

    [SerializeField] float timerMinimum = 1.5f;
    [SerializeField] float timerTickDownAmount = .1f;
    float countDownResetTime;
    int tickDownMultiplier = 1;

    bool isRunning;
    bool isActive;
    public bool IsActive
    {
        get { return isActive; }
    }
    bool shouldSpawn;
    public bool ShouldSpawn
    {
        get { return shouldSpawn; }
    }

	// Use this for initialization
	void Start ()
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
                    EventManagerOld.CallCellEvents();

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
    //        countDownTimer -= Time.deltaTime;

    //        if (countDownTimer <= .02f)
    //        {
    //            EventManager.CallCellEvents();

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
