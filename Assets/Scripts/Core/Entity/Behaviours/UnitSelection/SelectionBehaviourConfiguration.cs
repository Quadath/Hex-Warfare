namespace Core.Behaviours
{
    /*
     * A data container which is used for creation of corresponding behaviour.
     * This prevents Unity from direct creating of Behaviour instances.
     * It also helps to prevent Unity from providing runtime data.
     */
    public class SelectionBehaviourConfiguration: ISelectionBehaviour
    {
        public bool CanMove { get; }

        public SelectionBehaviourConfiguration(bool canMove)
        {
            CanMove = canMove;
        }
    }
}