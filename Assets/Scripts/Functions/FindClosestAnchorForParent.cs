using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine.XR.WSA;
using UnityEngine;

public class FindClosestAnchorForParent : MonoBehaviour
{
    GameObject[] anchorGameObjects;
    Anchor[] allAnchors;
    List<Anchor> objectAnchors;
    AnchorManager anchorManager;
    //WorldAnchor worldAnchor;

    private void Start()
    {
        anchorManager = GameObject.Find("AnchorManager").GetComponent<AnchorManager>();
        allAnchors = new Anchor[18];
        objectAnchors = new List<Anchor>();
        StartCoroutine(WaitThenFindParent());
    }

    private void OnDisable()
    {
        if(transform.parent != null)
            transform.parent = null;
    }

    IEnumerator WaitThenFindParent()
    {
        while (!anchorManager.CanFindAnchors)
            yield return null;

        gameObject.transform.SetParent(anchorManager.GetClosestAnchor(transform));
    }
}
