using System;
using Core;
using Core.Structs;

namespace SO
{
    [Serializable]
    //Used by scriptable objects.
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