using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Systems
{
    public class UnitView : MonoBehaviour
    {
        public Unit _unit;
        private bool isMoving = false;

        private void Start()
        {
            _unit = new Unit();
        }

        private void Update()
        {
            if (_unit == null) return;
            if (_unit.Path == null || _unit.Path.Count == 0) return;

            if (isMoving) return;
            StartCoroutine(Move());
        }

        IEnumerator Move()
        {
            isMoving = true;
            int cellIndex = 0;
            while (cellIndex < _unit.Path.Count)
            {
                Cell targetCell = _unit.Path[cellIndex];
            
                Debug.Log("Moving");
                if ((transform.position - Vector3Extensions.ToUnity(targetCell.Center)).sqrMagnitude < 0.01f)
                {
                    cellIndex++;
                    _unit.SetCell(targetCell);
                }
                else
                {
                    Vector3 targetPos = Vector3Extensions.ToUnity(targetCell.Center);
                    Vector3 up = transform.position.normalized;
                    Quaternion targetRotation = Quaternion.LookRotation(targetPos, up);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        2f * Time.deltaTime
                    );
                    transform.Translate((Vector3Extensions.ToUnity(targetCell.Center) - transform.position).normalized * (Time.deltaTime * 0.3f), Space.World);
                }
                yield return new WaitForEndOfFrame();
            }
            isMoving = false;
            _unit.StopMoving();
        }
    }
}
