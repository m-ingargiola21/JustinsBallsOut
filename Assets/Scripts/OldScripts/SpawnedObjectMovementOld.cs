using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class SpawnedObjectMovementOld : MonoBehaviour
{
    
    [SerializeField]
    float lerpTime = 1f;
    public float LerpTime
    {
        get { return lerpTime; }
        set { lerpTime = value; }
    }
    float currentLerpTime;

    bool shouldMove = true;
    Vector3 startPos;
    Vector3 endPos;

    Transform[] tweenTransforms;
    Vector3[] path;
    LTSpline spline;
    TweenPathRotator tweenPathRotator;
    bool isMoving;
    public bool IsMoving
    {
        get { return isMoving; }
    }

    protected void Start()
    {

        //uncomment below if using old movement
        //startPos = transform.position;
        //endPos = GameObject.Find("DespawnLocation").transform.position;

        tweenPathRotator = GameObject.Find("TweenPathRotator").GetComponent<TweenPathRotator>();

        tweenPathRotator.LookAtSpawnedObject(transform.gameObject);

        tweenTransforms = tweenPathRotator.GetComponentsInChildren<Transform>();

        path = new Vector3[] { tweenTransforms[1].position, tweenTransforms[1].position,
            tweenTransforms[2].position, tweenTransforms[3].position,
            tweenTransforms[4].position, tweenTransforms[4].position };

        spline = new LTSpline(path);

        LeanTween.moveSpline(transform.gameObject, path, lerpTime).setEase(LeanTweenType.easeInOutQuad).setOrientToPath(false);

        isMoving = true;

    }

    protected void Update()
    {



    }

    void OldMovement() //was in update
    {

        if (shouldMove)
        {
            //increment timer once per frame
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;

            float t = currentLerpTime / lerpTime;
            t = (t * t * t * (t * (6f * t - 15f) + 10f));

            transform.position = Vector3.Lerp(startPos, endPos, t);
        }

    }

    public void StartMovement()
    {

        shouldMove = true;
        
        LeanTween.resume(transform.gameObject);

        isMoving = true;

    }

    public void StopMovement()
    {

        shouldMove = false;
        
        LeanTween.pause(transform.gameObject);

        isMoving = false;

    }
}
