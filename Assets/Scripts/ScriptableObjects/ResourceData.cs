using System;
using Core;
using Core.Structs;

namespace SO
{
    [Serializable]
    public struct ResourceData
    {
        public ResourceTypes Type;
        public int Amount;

        public ResourceInstance ToCore()
        {
            return new ResourceInstance(Type, Amount);
        }
    }
}