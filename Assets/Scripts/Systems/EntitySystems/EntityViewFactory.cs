using System.Collections.Generic;
using System.Linq;
using Core;
using SO;
using UnityEngine;
using Utils;

namespace Systems
{
    public class EntityViewFactory: MonoBehaviour
    {
        [SerializeField] private EntityDataSORegistry _registry;
        private Dictionary<int, EntityDataSO> _data;

        void Start()
        {
            _data = _registry.Entries.ToDictionary(so => so.DefinitionId, so => so);
            GameBootstrap.Instance.Game.EntityCommands.SubscribeToOnEntityCreated(CreateView);
        }

        private void CreateView(Entity entity)
        {
            int id = entity.DefinitionId;
            EntityDataSO data = _data[id];
            GameObject prefab = data.Prefab;
            var gameObj = Instantiate(prefab, Vector3Extensions.ToUnity(entity.Position), Quaternion.identity);
            var view = gameObj.AddComponent<EntityView>();
            foreach (EntityBehaviourDataSO behaviourDataSo in data.Behaviours)
            {
                if (!behaviourDataSo.BehaviourView) continue;
                BehaviourView behaviourView = Instantiate(behaviourDataSo.BehaviourView, gameObj.transform).GetComponent<BehaviourView>();
                behaviourView.Init(entity.GetBehaviour(behaviourDataSo.BehaviourType), view);
                view.BehaviourViews.Add(behaviourView);
            }
            entity.SetViewId(view.GetEntityId());
            view.SetEntity(entity);
        }
    }
}