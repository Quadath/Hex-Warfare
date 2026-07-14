using System;
using UnityEngine;

public class RotateStarPivot : MonoBehaviour
{
    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, transform.up, Time.fixedDeltaTime);
    }
}
