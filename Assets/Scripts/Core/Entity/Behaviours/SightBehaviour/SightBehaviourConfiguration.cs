namespace Core.Behaviours
{
    public class SightBehaviourConfiguration: ISightBehaviour
    {
        public int Range { get; }

        public SightBehaviourConfiguration(int range)
        {
            Range = range;
        }
        public SightBehaviour Factory(Entity entity, Context ctx = null)
            => new (entity, this, ctx);
    }
}