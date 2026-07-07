using Shared;

namespace Core
{
    public abstract class DebugCommands
    {
        public abstract record DebugCommand;
        public record Message(string source, string text, int? instanceId = null) : DebugCommand;
        public record SphereCommand(Vector3Data center, float radius) : DebugCommand;
        public record LineCommand(Vector3Data start, Vector3Data end) : DebugCommand;
    }
}