using System.Collections.Generic;
using System.Linq;
using Core;
using Systems;
using Systems.Views;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Gameplay
{
    public class EntitySelectionSystem: GameplaySystem
    {
        [Header("UI")]
        public RectTransform selectionBox;
        public Canvas canvas;
        public Camera cam;

        [Header("Input Actions")]
        public InputActionProperty clickAction;
        public InputActionProperty positionAction;

        [Header("Settings")]
        public float dragThreshold = 20f; // pixels before we consider it a "drag"


        private Vector2 startPos;
        private Vector2 endPos;
        private Vector2 startScreenPos;
        private Vector2 endScreenPos;

        private bool isDragging = false;

        void OnEnable()
        {
            clickAction.action.Enable();
            positionAction.action.Enable();
        }

        void OnDisable()
        {
            clickAction.action.Disable();
            positionAction.action.Disable();
        }

        void UpdateSelectionBox()
        {
            RectTransform canvasRect = canvas.transform as RectTransform;

            // Convert screen coordinates to local canvas positions
            Vector2 startLocal;
            Vector2 endLocal;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, startScreenPos, canvas.worldCamera, out startLocal);
            RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, endScreenPos, canvas.worldCamera, out endLocal);

            Vector2 size = endLocal - startLocal;

            selectionBox.anchoredPosition = startLocal;
            selectionBox.sizeDelta = new Vector2(Mathf.Abs(size.x), Mathf.Abs(size.y));
            selectionBox.pivot = new Vector2(size.x >= 0 ? 0 : 1, size.y >= 0 ? 0 : 1);
        }


        Vector2 LocalToScreenPoint(RectTransform canvasRect, Vector2 localPoint)
        {
            Vector2 pivotOffset = new Vector2(
                canvasRect.rect.width * canvasRect.pivot.x,
                canvasRect.rect.height * canvasRect.pivot.y
            );
            Vector2 screenPoint = (Vector2)canvasRect.position + localPoint + pivotOffset;
            return screenPoint;
        }
        
        void SelectEntities()
        {
            Vector2 min = Vector2.Min(startScreenPos, endScreenPos);
            Vector2 max = Vector2.Max(startScreenPos, endScreenPos);

            List<SelectableView> entities = new List<SelectableView>();
            
            foreach (var view in FindObjectsOfType<SelectableView>())
            {
                Vector3 screenPos = cam.WorldToScreenPoint(view.transform.position);
                if (screenPos.z > 0 &&
                    screenPos.x > min.x && screenPos.x < max.x &&
                    screenPos.y > min.y && screenPos.y < max.y)
                {
                    // unit.SetSelected(true);
                    // if (unit.UnitCore.Player == 0)
                    // {
                    entities.Add(view);
                    // }
                }
                else
                {
                    // unit.SetSelected(false);
                }
            }
            
            Debug.Log(entities.Count);
            List<Entity> entityCores = entities
                .Select(view => view.Entity)
                .ToList();
            GameBootstrap.Instance.Game.EntityCommands.SelectEntities(entityCores);
            DebugUtils.Message(GetType().Name, $"Selected: {entityCores.Count} units.");
        }
            
        public override void Tick()
        {
            Vector2 mousePos = positionAction.action.ReadValue<Vector2>();

            if (clickAction.action.WasPressedThisFrame())
            {
                startScreenPos = mousePos;
                isDragging = false;
                selectionBox.gameObject.SetActive(false);
            }

            // Check if we’ve moved far enough to start dragging
            if (clickAction.action.IsPressed())
            {
                if (!isDragging)
                {
                    if (Vector2.Distance(mousePos, startScreenPos) > dragThreshold)
                    {
                        isDragging = true;
                        selectionBox.gameObject.SetActive(true);
                    }
                }

                if (isDragging)
                {
                    endScreenPos = mousePos;
                    UpdateSelectionBox();
                }
            }

            if (clickAction.action.WasReleasedThisFrame())
            {
                if (isDragging)
                {
                    SelectEntities();
                }

                isDragging = false;
                selectionBox.gameObject.SetActive(false);
            }
        }
    }
}