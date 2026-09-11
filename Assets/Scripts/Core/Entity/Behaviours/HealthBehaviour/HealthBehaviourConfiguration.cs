namespace Core.Behaviours
{
    public class HealthBehaviourConfiguration: IHealthBehaviour
    {
        public int MaxHealth { get; }
        
        public HealthBehaviourConfiguration(int maxHealth)
        {
            this.MaxHealth = maxHealth;
        }
        public HealthBehaviour Factory(Entity entity, Context ctx = null)
            => new (entity, this, ctx);
    }
}