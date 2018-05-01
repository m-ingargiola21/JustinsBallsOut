using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public delegate void EventSpawns(Vector3 vector3);
    public static event EventSpawns OnTimerAction;
    public static event EventSpawns OnRaycastAction;
    public static event EventSpawns OnTutorialFoodSpawn;

    public delegate void EventBullets(GameObject gameObject, bool isBool);
    public static event EventBullets OnBulletMissed;
    public static event EventBullets OnBulletHit;

    public delegate void EventSnake(GameObject gameObject);
    public static event EventSnake OnFoodSpawn;
    public static event EventSnake OnFoodTypeChoice;
    public static event EventSnake OnSnakeSpawn;

    public delegate void EventDespawn(GameObject gameObject);
    public static event EventDespawn OnFoodDespawn;

    public delegate void EventFoodEaten(bool isBool);
    public static event EventFoodEaten OnFoodEaten;

    public delegate void EventGameManager(bool isBool);
    public static event EventGameManager OnTutorialEnd;
    public static event EventGameManager OnGameOver;
    public static event EventGameManager OnGameRestart;
    public static event EventGameManager OnRoundOver;

    public delegate void EventScore(int Int);
    public static event EventScore OnPlayerScored;
    public static event EventScore OnSnakeScoreMultiplier;
    public static event EventScore OnBulletHitScore;

    public delegate void EventScoreDisplay(Vector3 vector3);
    public static event EventScoreDisplay OnDisplayScore;

    public delegate void EventMultiplierDisplay(int i, Vector3 vector3);
    public static event EventMultiplierDisplay OnDisplayMultiplier;

    public delegate void EventFoodChoice(GameObject gameObject);
    public static event EventFoodChoice OnTypeOfFoodChoosen;

    public delegate void EventUIButtons(Transform transform);
    public static event EventUIButtons OnButtonPress;

    public delegate void EventTouchInput(Transform transform);
    public static event EventTouchInput OnUITouch;
    public static event EventTouchInput OnFoodTouch;
    public static event EventTouchInput OnSnakeTouch;
    public static event EventTouchInput OnBoundaryTouch;

    public static void CallBoundaryTouch(Transform t)
    {
        if (OnBoundaryTouch != null)
            OnBoundaryTouch(t);
    }

    public static void CallSnakeTouch(Transform t)
    {
        if (OnSnakeTouch != null)
            OnSnakeTouch(t);
    }

    public static void CallFoodTouch(Transform t)
    {
        if (OnFoodTouch != null)
            OnFoodTouch(t);
    }

    public static void CallUITouch(Transform t)
    {
        if (OnUITouch != null)
            OnUITouch(t);
    }

    public static void CallDisplayMultiplier(int i, Vector3 v)
    {
        if (OnDisplayMultiplier != null)
            OnDisplayMultiplier(i, v);
    }

    public static void CallTypeOfFoodChoosen(GameObject go)
    {
        if (OnTypeOfFoodChoosen != null)
            OnTypeOfFoodChoosen(go);
    }

    public static void CallDisplayScore(Vector3 v)
    {
        if (OnDisplayScore != null)
            OnDisplayScore(v);
    }

    public static void CallBulletHitScore(int i)
    {
        if (OnBulletHitScore != null)
            OnBulletHitScore(i);
    }

    public static void CallSnakeScoreMultiplier(int i)
    {
        if (OnSnakeScoreMultiplier != null)
            OnSnakeScoreMultiplier(i);
    }

    public static void CallPlayerScored(int i)
    {
        if (OnPlayerScored != null)
            OnPlayerScored(i);
    }

    public static void CallRoundOver(bool b)
    {
        if (OnRoundOver != null)
            OnRoundOver(b);
    }

    public static void CallGameOver(bool b)
    {
        if (OnGameOver != null)
            OnGameOver(b);
    }

    public static void CallGameRestart(bool b)
    {
        if (OnGameRestart != null)
            OnGameRestart(b);
    }

    public static void CallTutorialEnd(bool b)
    {
        if (OnTutorialEnd != null)
            OnTutorialEnd(b);
    }

    public static void CallFoodEaten(bool b)
    {
        if (OnFoodEaten != null)
            OnFoodEaten(b);
    }

    public static void CallFoodDespawn(GameObject go)
    {
        if (OnFoodDespawn != null)
            OnFoodDespawn(go);
    }

    public static void CallFoodTypeChoice(GameObject go)
    {
        if (OnFoodTypeChoice != null)
            OnFoodTypeChoice(go);
    }
    
    public static void CallFoodSpawn(GameObject go)
    {
        if (OnFoodSpawn != null)
            OnFoodSpawn(go);
    }

    public static void CallSnakeSpawn(GameObject go)
    {
        if (OnSnakeSpawn != null)
            OnSnakeSpawn(go);
    }

    public static void CallBulletMissed(GameObject go, bool b)
    {
        if (OnBulletMissed != null)
            OnBulletMissed(go, b);
    }

    public static void CallBulletHit(GameObject go, bool b)
    {
        if (OnBulletHit != null)
            OnBulletHit(go, b);
    }

    public static void CallTutorialFoodSpawn(Vector3 v)
    {
        if (OnTutorialFoodSpawn != null)
            OnTutorialFoodSpawn(v);
    }

    public static void CallTimerAction(Vector3 v)
    {
        if (OnTimerAction != null)
            OnTimerAction(v);
    }

    public static void CallRaycastAction(Vector3 v)
    {
        if (OnRaycastAction != null)
            OnRaycastAction(v);
    }


}
