using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Utils;
using UnityEngine;

namespace Systems
{
    public class UnitView : MonoBehaviour
    {
        private Unit Unit { get; set;}
        
        internal void SetUnit(Unit unit)
        {
            Unit = unit;
            transform.position = Vector3Extensions.ToUnity(Unit.Position);
            Vector3 up = transform.position.normalized;
            Vector3 dir = Vector3Extensions.ToUnity(Unit.ToLookAt - Unit.Position);
            transform.rotation = Quaternion.LookRotation(dir, up);
        }
        
        private void FixedUpdate()
        {
            if(Unit == null) return;
            transform.position = Vector3Extensions.ToUnity(Unit.Position);
            Vector3 up = transform.position.normalized;
            Vector3 dir = Vector3Extensions.ToUnity(Unit.ToLookAt - Unit.Position); 
            Quaternion targetRotation = Quaternion.LookRotation(dir, up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                2f * Time.deltaTime
            );
        }
    }
}
