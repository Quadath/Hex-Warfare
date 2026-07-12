using System;

namespace Core
{
    public interface ITargetable
    {
        public void AddOnDeathListener(Action<ITargetable> listener);
        public void RemoveOnDeathListener(Action<ITargetable> listener);
    }
}