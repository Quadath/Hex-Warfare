namespace Core.Behaviours
{
    public class LandUnitMovementBehaviourConfiguration: ILandUnitMovementBehaviour
    {
        public float BaseSpeed { get; }
        
        public LandUnitMovementBehaviourConfiguration(float baseSpeed) 
        {
            this.BaseSpeed = baseSpeed;
        }
        public LandUnitMovementBehaviour Factory(Entity entity, Context ctx = null) 
            => new (entity, this, ctx);
    }
}