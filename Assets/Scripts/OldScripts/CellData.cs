using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellData {

    private Vector3 cellPos;
    private Vector3 cellDirection;
    int cellHealth;
    bool immune;

    public bool IsImmunized
    {
        get { return immune; }
        set { immune = value; }
    }
    public Vector3 CellPos
    {
        get { return cellPos; }
        set { cellPos = value; }
    }
    public Vector3 CellDirection
    {
        get { return cellDirection; }
        set { cellDirection = value; }
    }

    public int CellHealth
    {
        get { return cellHealth; }
        set { cellHealth = value; }
    }

    public void Setup()
    {
        cellHealth = 3;
    }

 //   private void Awake()
 //   {
        
 //   }
 //   // Use this for initialization
 //   void Start () {
 //       cellHealth = 3;
 //   }
	
	//// Update is called once per frame
	//void Update () {
		
	//}

    public void TakeDamage()
    {
        cellHealth--;
    }
}
