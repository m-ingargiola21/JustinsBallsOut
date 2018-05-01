using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    //TODO: 
    //comment with what our scripts will need to do not with the tutorial shit


    private IState currentlyRunningState;
    private IState previousState;


    public void ChangeState(IState newState)
    {
        if(this.currentlyRunningState != null)
        {
            this.currentlyRunningState.Exit();//might give null reference so we use the if
        }
        this.previousState = this.currentlyRunningState;

        this.currentlyRunningState = newState;
        this.currentlyRunningState.Enter();

    }//end ChangeState()

    public void ExecuteStateUpdate()
    {
        var runningState = this.currentlyRunningState;
        if (runningState != null)
            runningState.Execute();

    }//end ExecuteStateUpdate()

    public void SwitchToPreviousState()
    {
        this.currentlyRunningState.Exit();
        this.currentlyRunningState = this.previousState;
        this.currentlyRunningState.Enter();

    }




}
    
