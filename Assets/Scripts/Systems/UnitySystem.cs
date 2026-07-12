using Core;
using UnityEngine;

namespace Systems
{
    /*Allows to create UnitySystems in certain order and initialise them in
     certain order. Also prevents NullException with Game.
     The List<UnitySystem> inside GameBootstrap.cs is serialized and defines the order.*/
    public abstract class UnitySystem: MonoBehaviour
    {
        public abstract void Init(Game game);
    }
}