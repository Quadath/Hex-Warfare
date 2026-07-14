namespace Core
{
    public class ResourceInstance
    {
        public readonly ResourceTypes Type;
        public int Amount { get; private set; }

        public ResourceInstance(ResourceTypes type, int amount = 0)
        {
            Type = type;
            Amount = amount;
        }

        internal void AddAmount(int amount) => Amount += amount;

        internal bool TryWithdraw(int amount)
        {
            if (Amount - amount < 0) return false;
            Amount -= amount;
            return true;
        }
    }
}