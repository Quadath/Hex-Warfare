using System;
using Systems;
using UnityEngine;
using Utils;

public class BuildingAligner : MonoBehaviour
{
    private void Start()
    {
        var view = GetComponent<EntityView>();
        transform.rotation = Quaternion.LookRotation(Vector3Extensions.ToUnity(view.Entity.Cell.Center),transform.position);
    }
}
