using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using Utils;
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
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        public Func<UnitData> UnitDataFactory()
        {
            return () => new UnitData(name, unitType, 
                behaviours.Select(so => so.UnitBehaviourFactory()).ToList());
        }
    }
}
