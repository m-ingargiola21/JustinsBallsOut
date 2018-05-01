using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //TODO: 
    //comment with what our scripts will need to do not with the tutorial shit

    void Enter();
    void Execute();
    void Exit();
	
}
