using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AnchorObject : MonoBehaviour
{

    private Anchor anchor;
    private Pose pose;
    public Trackable trackable;
    
    private void Start()
    {
        pose.position = transform.position;
        pose.rotation = transform.rotation;

        anchor = Session.CreateAnchor(pose, trackable);
    }
    
}
