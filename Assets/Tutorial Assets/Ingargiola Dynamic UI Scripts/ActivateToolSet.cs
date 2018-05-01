using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateToolSet : MonoBehaviour
{
    public float scaleFactor = 0.4f;
    GameObject cursor;
    bool isActive;

    public List<SpriteRenderer> rend;

	// Use this for initialization
	void Start ()
    {
        cursor = GameObject.Find("Cursor");
        transform.localScale *= scaleFactor;
	}
	
    private void OnEnable()
    {
        NRSRManager.ObjectFocused += ActivateThis;
        NRSRManager.ObjectUnFocused += DeactivateThis;
    }

    private void OnDisable()
    {
        NRSRManager.ObjectFocused -= ActivateThis;
        NRSRManager.ObjectUnFocused -= DeactivateThis;
    }


    void ActivateThis()
    {
        //Debug.Log(gameObject.name +  "ATS ActivateThis");
        foreach (SpriteRenderer spriteRend in rend)
        {
            Debug.Log(gameObject.name + "ATS ActivateThis");
            spriteRend.enabled = true;
        }
    }

    void DeactivateThis()
    {

        foreach (SpriteRenderer spriteRend in rend)
        {
            Debug.Log(gameObject.name + "ATS DeActivateThis");
            spriteRend.enabled = false;
        }
    }
}//end ActivateToolSet class
