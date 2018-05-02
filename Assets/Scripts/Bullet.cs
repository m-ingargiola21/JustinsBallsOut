using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour, ISubscriber
{
    [SerializeField] private float lerpTime = 2f;
    [SerializeField] private float lerpDistance = 55f;
    [SerializeField] private AudioClip hitClip;
    [SerializeField] private Transform tendrils;

    private bool hasHitFood;
    public bool HasHitFood { get { return hasHitFood; } set { hasHitFood = value; } }
    private float currentLerpTime;
    private AudioSource audioSource;
    private Vector3 startPos;
    private Vector3 endPos;

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
    }

    void Start ()
    {
        hasHitFood = false;
        audioSource = transform.GetComponent<AudioSource>();
        audioSource.clip = hitClip;

        transform.position = Camera.main.transform.position;
        transform.rotation = Camera.main.transform.rotation;
        startPos = transform.position;
        endPos = transform.position + transform.forward * lerpDistance;

        StartCoroutine(MoveToEndThenDestroy());
	}
	
    IEnumerator MoveToEndThenDestroy()
    {
        while (transform.position != endPos)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
            {
                currentLerpTime = lerpTime;
            }

            //lerp!
            float perc = currentLerpTime / lerpTime;
            transform.Rotate(new Vector3(0f, 0f, 15f) * currentLerpTime);
            transform.position = Vector3.Lerp(startPos, endPos, perc);
            yield return null;
        }

        if(!hasHitFood)
        {
            EventManager.CallBulletMissed(this.gameObject, true);
        }
        
        Destroy(gameObject);
    }

    public void HitFood(GameObject bulletToDestroy, bool b)
    {
        if(bulletToDestroy == this.gameObject)
        {
            Destroy(gameObject);
        }
    }

    public void Subscribe()
    {
        EventManager.OnBulletHit += HitFood;
    }

    public void Unsubscribe()
    {
        EventManager.OnBulletHit -= HitFood;
    }
}
