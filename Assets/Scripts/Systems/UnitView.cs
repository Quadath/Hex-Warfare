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
        public Unit Unit { get; private set;}
        

        internal void SetUnit(Unit unit)
        {
            Unit = unit;
            previousPosition = Vector3Extensions.ToUnity(Unit.Cell.Neighbors[0].Center); //delete
        }

        private Vector3 previousPosition; //rework
        private void Update()
        {
            if(Unit == null) return;
            transform.position = Vector3Extensions.ToUnity(Unit.Position);
            Vector3 targetPos = transform.position + (transform.position - previousPosition).normalized; //rework
            Vector3 up = transform.position.normalized;
            Quaternion targetRotation = Quaternion.LookRotation(targetPos, up);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                2f * Time.deltaTime
            );
            previousPosition = transform.position;
        }

        // IEnumerator Move()
        // {
        //     isMoving = true;
        //     int cellIndex = 0;
        //     while (cellIndex < _unit.Path.Count)
        //     {
        //         Cell targetCell = _unit.Path[cellIndex];
        //     
        //         Debug.Log("Moving");
        //         if ((transform.position - Vector3Extensions.ToUnity(targetCell.Center)).sqrMagnitude < 0.01f)
        //         {
        //             cellIndex++;
        //             _unit.SetCell(targetCell);
        //         }
        //         else
        //         {
        //             Vector3 targetPos = Vector3Extensions.ToUnity(targetCell.Center);
        //             Vector3 up = transform.position.normalized;
        //             Quaternion targetRotation = Quaternion.LookRotation(targetPos, up);
        //             transform.rotation = Quaternion.Slerp(
        //                 transform.rotation,
        //                 targetRotation,
        //                 2f * Time.deltaTime
        //             );
        //             transform.Translate((Vector3Extensions.ToUnity(targetCell.Center) - transform.position).normalized * (Time.deltaTime * 0.3f), Space.World);
        //         }
        //         yield return new WaitForEndOfFrame();
        //     }
        //     isMoving = false;
        //     _unit.StopMoving();
        // }
    }
}
