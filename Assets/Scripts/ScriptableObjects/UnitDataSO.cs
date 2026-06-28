using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;


namespace SO
{
    [CreateAssetMenu(fileName = "UnitDataSO", menuName = "Unit/UnitData")]
    public class UnitDataSO : ScriptableObject
    {
        [SerializeField] private new string name;
        [SerializeField] private UnitTypes unitType;
        public UnitTypes UnitType => unitType;
        [SerializeField] 
        List<UnitBehaviourSO> behaviours;
        [SerializeField] GameObject prefab;

        public Func<UnitData> UnitDataFactory()
        {
            return () => new UnitData(name, unitType, OnCreated, 
                behaviours.Select(so => so.UnitBehaviourFactory()).ToList());
        }

        private void OnCreated(Unit unit)
        {
            Debug.Log("Unit created!");
            Instantiate(prefab, Vector3.zero, Quaternion.identity);
        }
    }
}
