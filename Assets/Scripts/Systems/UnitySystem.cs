using Core;
using UnityEngine;

namespace Systems
{
    public abstract class UnitySystem: MonoBehaviour
    {
        public abstract void Init(Game game);
    }
}