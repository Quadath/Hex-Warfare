namespace Core.Behaviours
{
    public class SelectionBehaviour: Behaviour, ISelectionBehaviour
    {
        public bool CanMove { get; internal set; }
        
        /*|||||||||
         * RUNTIME
         ||||||||*/
        public SelectionBehaviour(Entity owner, ISelectionBehaviour conf, Context ctx = null) : base(owner, ctx)
        {
            CanMove = conf.CanMove;
        }
    }
}