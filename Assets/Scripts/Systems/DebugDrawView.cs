using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

namespace Systems
{
    public class DebugDrawView : MonoBehaviour
    {
        private readonly Queue<DebugCommands.DebugCommand> _commands = new();

        private void OnEnable()
        {
            DebugUtils.Command += ReceiveCommand;
        }

        private void OnDisable()
        {
            DebugUtils.Command -= ReceiveCommand;
        }

        private void ReceiveCommand(DebugCommands.DebugCommand command)
        {
            _commands.Enqueue(command);
        }

        private void OnDrawGizmos()
        {
            while (_commands.Count > 0)
            {
                Draw(_commands.Dequeue());
            }
        }

        private static void Draw(DebugCommands.DebugCommand command)
        {
            switch (command)
            {
                case DebugCommands.Message msg: 
                    Debug.Log($"[<color=green>{msg.source}</color>] |{msg.instanceId}| {msg.text}");
                    break;
                case DebugCommands.LineCommand line:
                    Gizmos.DrawLine(
                        Vector3Extensions.ToUnity(line.start),
                        Vector3Extensions.ToUnity(line.end));
                    break;

                case DebugCommands.SphereCommand sphere:
                    Gizmos.DrawSphere(
                        Vector3Extensions.ToUnity(sphere.center),
                        sphere.radius);
                    break;
            }
        }
    }
}