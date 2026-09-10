using System;
using Systems;
using UnityEngine;
using Utils;

public class BuildingAligner : MonoBehaviour
{
    private void Start()
    {
        var view = GetComponent<EntityView>(); //Might be null!!
        transform.rotation = Quaternion.LookRotation(transform.position - Vector3Extensions.ToUnity(view.Entity.Cell.Center),
            transform.position);
    }
}
