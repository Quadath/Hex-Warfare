using System.Collections.Generic;
using UnityEngine;

namespace SO
{
    //Holds ScriptableObjects every entity
    [CreateAssetMenu(menuName = "SO/EntityDataSORegistry")]
    public class EntityDataSORegistry: ScriptableObject
    {
        [SerializeField] private List<EntityDataSO> entries;
        public List<EntityDataSO> Entries => entries;
    }
}