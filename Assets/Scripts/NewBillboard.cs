// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBillboard : MonoBehaviour
{
    public enum PivotAxis
    {
        //Rotate anout all axes.
        Free,
        //Rotate about an individual axis.
        Y
    }

    [Tooltip("Specifies the axis about which the object will rotate.")]
    public PivotAxis pivotAxis = PivotAxis.Free;

    [Tooltip("Specifies the target we will orient to.  If no Target it specified the main camera will be used.")]
    public Transform TargetTransform;

    private void OnEnable()
    {
        if (TargetTransform == null)
        {
            TargetTransform = Camera.main.transform;
        }

        Update();
    }

    private void Update()
    {
        if (TargetTransform == null)
        {
            return;
        }

        Vector3 directionToTarget = TargetTransform.position - transform.position;

        switch(pivotAxis)
        {
            case PivotAxis.Y:
                directionToTarget.y = 0.0f;
                break;
            case PivotAxis.Free:
            default:
                //No changes needed.
                break;
        }

        if (directionToTarget.sqrMagnitude < 0.001f)
        {
            return;
        }

        transform.rotation = Quaternion.LookRotation(-directionToTarget);
    }
}
