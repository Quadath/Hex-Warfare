using Core.Commands;

namespace Core
{
    public sealed class Game
    {
        private readonly UnitSystemsContainer _unitSystems = new();
        private readonly UnitManager _unitManager = new();
        private UnitFactory _unitFactory;
        
        public UnitCommands UnitCommands { get; }

        public Game()
        {
            _unitFactory = new UnitFactory(_unitSystems, _unitManager);
            UnitCommands = new UnitCommands(_unitFactory, _unitManager,  _unitSystems.MovementSystem);
        }
    }
}