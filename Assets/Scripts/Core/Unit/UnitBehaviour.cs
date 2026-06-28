namespace Core
{
    public abstract class UnitBehaviour
    {
        private Unit _owner;

        protected UnitBehaviour(Unit owner, Context ctx = null)
        {
            _owner = owner;
        }
    }
}