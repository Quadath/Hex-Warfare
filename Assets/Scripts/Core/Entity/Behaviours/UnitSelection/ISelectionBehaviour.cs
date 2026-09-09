namespace Core.Behaviours
{
    //Interfaces DO NOT declare runtime properties
    public interface ISelectionBehaviour
    {
        public bool CanMove { get; }
    }
}