using System;
using Shared;

namespace Core
{
    public static class DebugUtils
    {
        public static event Action<DebugCommands.DebugCommand>? Command;

        public static void Message(string source, string text, int? instanceId = null)
        {
            Command?.Invoke(new DebugCommands.Message(source, text, instanceId));
        }

        public static void Line(Vector3Data start, Vector3Data end)
        {
            Command?.Invoke(new DebugCommands.LineCommand(start, end));
        }

        public static void Sphere(Vector3Data center, float radius)
        {
            Command?.Invoke(new DebugCommands.SphereCommand(center, radius));
        }
    }
}