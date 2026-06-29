namespace Core.Commands
{
    public class WorldCommands
    {
        private readonly Planet _planet;
        
        internal WorldCommands(Planet planet)
        {
            _planet = planet;
        }

        public Planet GeneratePlanet(int subdivisions, float radius)
        {
            _planet.Generate(subdivisions, radius);
            return _planet;
        }
    }
}