using System.Collections;
using System.Collections.Generic;
using GoogleARCore;
using UnityEngine;

public class AnchorManager : MonoBehaviour
{
    [SerializeField] private Transform[] transforms;

    private bool canFindAnchors;
    public bool CanFindAnchors { get { return canFindAnchors; } }
    private Anchor[] anchors;
    private Pose[] poses;

	// Use this for initialization
	void Start ()
    {
        anchors = new Anchor[36];
        poses = new Pose[36];

        StartCoroutine(WaitForStart());
	}

    IEnumerator WaitForStart()
    {
        while (Session.Status != SessionStatus.Tracking)
            yield return null;

        canFindAnchors = true;
        Anchor anchor;
        anchor = Session.CreateAnchor(new Pose(transform.position, transform.rotation));
        
        Debug.Log(anchor.ToString());

        for (int i = 0; i < transforms.Length; i++)
        {
            poses[i].position = transforms[i].position;
            poses[i].rotation = transforms[i].rotation;
            anchors[i] = Session.CreateAnchor(poses[i]);
        }
    }

    public Transform GetClosestAnchor(Transform transformToChild)
    {
        Anchor closestAnchor;

        closestAnchor = anchors[0];

        foreach (Anchor a in anchors)
        {
            if (Mathf.Abs((transform.position - a.transform.position).magnitude) <
               Mathf.Abs((transform.position - closestAnchor.transform.position).magnitude))
            {
                closestAnchor = a;
            }
        }

        return closestAnchor.transform;
    }
}
