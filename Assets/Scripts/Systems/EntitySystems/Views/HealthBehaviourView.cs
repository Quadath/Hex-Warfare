using System;
using Core;
using Core.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Systems.Views
{
    public class HealthBehaviourView: BehaviourView
    {
        [SerializeField] private GameObject healthBarPrefab;
        
        private HealthBehaviour _behaviour;
        private Slider _healthBar;
        private Canvas _canvas;
        private Transform _cameraTransform;
        
        protected override void OnInit()
        {
            _behaviour = Behaviour as HealthBehaviour;
            _canvas = FindAnyObjectByType<Canvas>();
            _healthBar = Instantiate(healthBarPrefab, _canvas.transform).GetComponent<Slider>();
            _cameraTransform = Camera.main.transform;
            if (!_cameraTransform) throw new NullReferenceException("Camera is null!");
            _behaviour.AddOnDamageListener(UpdateHealthBar);
            _behaviour.Owner.AddOnDestroyedListener(HandleEntityDeath);
            UpdateHealthBar(_behaviour);
        }
        public override void Tick()
        {
            UpdateHealthBarPosition();
        }

        private void UpdateHealthBar(HealthBehaviour behaviour)
        {
            _healthBar.value = (float)_behaviour.Health /  behaviour.MaxHealth;
        }

        private void UpdateHealthBarPosition()
        {

            Vector3 worldPosition = transform.position;
            // Move it below the entity.
            worldPosition -= _cameraTransform.up * 0.1f;

            Vector3 screenPosition =
                Camera.main.WorldToScreenPoint(worldPosition);
            _healthBar.transform.position = screenPosition;

        }

        private void HandleEntityDeath(Entity entity)
        {
            _behaviour.RemoveOnDamageListener(UpdateHealthBar);
            _behaviour.Owner.RemoveOnDestroyedListener(HandleEntityDeath);
        }
    }
}