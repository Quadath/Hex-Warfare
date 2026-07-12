namespace Core.Behaviours
{
    public class SelectionBehaviour: Behaviour
    {
        internal readonly bool CanMove;
        /*|||||||||
         * RUNTIME
         ||||||||*/
        public SelectionBehaviour(Entity owner, bool canMove, Context ctx = null) : base(owner, ctx)
        {
            CanMove = canMove;
        }
    }
}