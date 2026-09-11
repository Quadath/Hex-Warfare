using System;
using System.Collections.Generic;
using Core;
using UnityEngine;
using Utils;

namespace Systems
{
    public class DebugView : MonoBehaviour
    {
        private readonly List<ActiveGizmo> _activeGizmos = new();

        private void OnEnable()
        {
            DebugUtils.TextCommand += ReceiveTextCommand;
            DebugUtils.GizmosCommand += ReceiveGizmosCommand;
        }

        private void OnDisable()
        {
            DebugUtils.TextCommand -= ReceiveTextCommand;
            DebugUtils.GizmosCommand -= ReceiveGizmosCommand;
        }

        private void ReceiveTextCommand(DebugCommands.TextDebugCommand command)
        {
            switch (command)
            {
                case DebugCommands.Message msg: 
                    Debug.Log($"[<color=green>{msg.source}</color>] |{msg.instanceId}| {msg.text}");
                    break;
            }
        }

        private void ReceiveGizmosCommand(DebugCommands.GizmosDebugCommand command)
        {
            _activeGizmos.Add(new ActiveGizmo
            {
                command = command,
                expirationTime = command.lifetime.HasValue
                    ? Time.time + command.lifetime.Value
                    : float.PositiveInfinity
            });
        }

        private void Update()
        {
            for (int i = _activeGizmos.Count - 1; i >= 0; i--)
            {
                if (Time.time > _activeGizmos[i].expirationTime)
                    _activeGizmos.RemoveAt(i);
            }
        }

        private void OnDrawGizmos()
        {
            foreach (var gizmo in _activeGizmos)
            {
                Draw(gizmo.command);
            }
        }
        //Commands are rendered just for one frame
        private static void Draw(DebugCommands.GizmosDebugCommand command)
        {
            switch (command)
            {
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
        private class ActiveGizmo
        {
            public DebugCommands.GizmosDebugCommand command;
            public float expirationTime;
        }
    }
}