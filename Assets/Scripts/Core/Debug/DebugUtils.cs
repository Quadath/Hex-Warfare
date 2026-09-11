using System;
using System.Collections.Generic;
using Core.Structs;

namespace Core
{
    public static class DebugUtils
    {
        private static readonly Queue<DebugCommands.TextDebugCommand> _pending = new();
        public static event Action<DebugCommands.TextDebugCommand> TextCommand
        {
            add
            {
                //Send everything that happened before the listener existed.
                while(_pending.Count > 0)
                    value.Invoke(_pending.Dequeue());
                _command += value;
            }
            remove => _command -= value;
        }
        private static Action<DebugCommands.TextDebugCommand> _command;
        public static event Action<DebugCommands.GizmosDebugCommand> GizmosCommand;
        
        public static void Message(Object sender, string text, int? instanceId = null)
        {
            Send(new DebugCommands.Message(sender.GetType().Name, text, instanceId));
        }
        public static void Message(string sender, string text, int? instanceId = null)
        {
            Send(new DebugCommands.Message(sender, text, instanceId));
        }

        public static void Line(Vector3Data start, Vector3Data end, float? lifetime = 0f)
        {
            GizmosCommand?.Invoke(new DebugCommands.LineCommand(start, end, lifetime));
        }

        public static void Sphere(Vector3Data center, float radius, float? lifetime = 0f)
        {
            GizmosCommand?.Invoke(new DebugCommands.SphereCommand(center, radius, lifetime));
        }

        private static void Send(DebugCommands.TextDebugCommand command)
        {
            if (_command != null)
                _command.Invoke(command);
            else
                _pending.Enqueue(command);
        }
    }
}