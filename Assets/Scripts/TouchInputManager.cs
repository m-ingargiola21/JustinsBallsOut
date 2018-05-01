using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TouchInputManager : MonoBehaviour
{
    //[SerializeField] TextMesh textMesh;
    Touch touch;
    bool isCoroutineRunning = false;
    float delayTime = 1.0f;

    WaitForSeconds delay;

    private void Start()
    {
        delay = new WaitForSeconds(delayTime);
    }
    
    void Update ()
    {
        if (Input.touchCount > 0)
        {
            if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touch = Input.GetTouch(0);

                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                RaycastHit raycastHit;

                if (Physics.Raycast(ray, out raycastHit, Mathf.Infinity))
                {

                    if (raycastHit.transform.tag == "UI")
                    {
                        EventManager.CallUITouch(raycastHit.transform);
                    }

                    //if (SceneManager.GetActiveScene().name == "_Justin" &&
                    //    //GameController.instance._GameState !=
                    //    //GameController.GameStates.GamePause)
                    //{
                    //    if (raycastHit.transform.tag == "Food")
                    //    {
                    //        EventManager.CallFoodTouch(raycastHit.transform);
                    //    }
                    //    else if (raycastHit.transform.tag == "Predators")
                    //    {
                    //        EventManager.CallSnakeTouch(raycastHit.transform);
                    //    }
                    //    else if (raycastHit.transform.tag == "Boundary" && 
                    //        //GameController.instance._GameState != GameController.GameStates.GamePause ||
                    //        //GameController.instance._GameState != GameController.GameStates.GameOver)
                    //    {
                    //        raycastHit.transform.position = raycastHit.point;
                    //        EventManager.CallBoundaryTouch(raycastHit.transform);
                    //    }
                    //}
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Stationary)
            {
                if (SceneManager.GetActiveScene().name == "_Justin")
                {
                    //if (!isCoroutineRunning &&
                    //    //GameController.instance._GameState !=
                    //    //GameController.GameStates.GamePause)
                    //{
                    //    StartCoroutine(WaitThenPauseGame());
                    //}
                }
            }
            else if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (SceneManager.GetActiveScene().name == "_Justin")
                {
                    //if (GameController.instance._GameState !=
                    //    //GameController.GameStates.GamePause)
                    //{
                    //    StopAllCoroutines();
                    //    isCoroutineRunning = false;
                    //}
                }
            }
        }   
    }

    IEnumerator WaitThenPauseGame()
    {
        isCoroutineRunning = true;

        yield return delay;
        
        //GameController.instance.PauseGame();

        isCoroutineRunning = false;
    }
}

