using Core.Structs;

namespace Core
{
    public abstract class DebugCommands
    {
        public abstract record TextDebugCommand;
        public abstract record GizmosDebugCommand(float? lifetime);
        public record Message(string source, string text, int? instanceId = null) : TextDebugCommand;
        public record SphereCommand(Vector3Data center, float radius, float? lifetime = 0f) : GizmosDebugCommand(lifetime);
        public record LineCommand(Vector3Data start, Vector3Data end, float? lifetime = 0f) : GizmosDebugCommand(lifetime);
    }
}