namespace Core.Behaviours
{
    //Container of data. This data is used in Tick() of corresponding system.
    public class HealthBehaviour: Behaviour, IHealthBehaviour
    {
        public int MaxHealth { get; }
        /*|||||||||
         * RUNTIME
         ||||||||*/
        public int Health { get; private set; }
        
        //Context contains things a Behaviour may need (e.g. another Behaviour)
        internal HealthBehaviour(Entity owner, IHealthBehaviour config, Context ctx = null): base(owner)
        {
            MaxHealth = config.MaxHealth;
            Health = config.MaxHealth;
        }
    }
}