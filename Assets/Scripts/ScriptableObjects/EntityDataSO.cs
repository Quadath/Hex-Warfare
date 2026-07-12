using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Core;

namespace SO
{
    [CreateAssetMenu(menuName = "SO/EntityData")]
    public class EntityDataSO: ScriptableObject
    {
        [SerializeField] private int definitionId;
        public int DefinitionId => definitionId;
        [SerializeField] private string entityName;
        [SerializeField] private GameObject prefab;
        public GameObject Prefab => prefab;
        
        [SerializeField] private List<EntityBehaviourDataSO> behaviours;
        public List<EntityBehaviourDataSO> Behaviours => behaviours;

        public Func<EntityData> EntityDataFactory()
        {
            return () => new EntityData(name, definitionId, 
                behaviours.Select(so => so.BehaviourFactory()).ToList());
        }
    }
}