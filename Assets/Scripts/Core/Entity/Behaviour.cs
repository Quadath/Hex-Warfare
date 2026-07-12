namespace Core
{
    public abstract class Behaviour
    {
        public readonly Entity Owner;

        //Context may contain things a Behaviour needs (e.g. another Behaviour)
        protected Behaviour(Entity owner, Context ctx = null)
        {
            Owner = owner;
        }
    }
}