using System;
using Core;
using SO;
using UnityEngine;
using Utils;

namespace Systems
{
    public class UnitViewFactory: MonoBehaviour, IUnitViewFactory
    {
        [Obsolete("Obsolete")]
        public void CreateView(UnitDataSO so, Unit unit)
        {
            var view = Instantiate(so.Prefab, Vector3Extensions.ToUnity(unit.Position), Quaternion.identity).AddComponent<UnitView>();
            unit.SetViewInstanceId(view.GetEntityId());
            view.SetUnit(unit);
        }
    }
}