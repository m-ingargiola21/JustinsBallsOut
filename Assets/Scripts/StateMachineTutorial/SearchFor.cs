using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchFor : IState
{
    //TODO: 
    //comment with what our scripts will need to do not with the tutorial shit

    private LayerMask searchLayer;

    //constructor
    public SearchFor(LayerMask searchLayer)
    {
        this.searchLayer = searchLayer;
    }

    public void Enter()
    {

    }
    public void Execute()
    {

    }
    public void Exit()
    {

    }


}//end SearchFor class
