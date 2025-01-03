using System;
using CodeBase.Enemy;
using CodeBase.Services;
using CodeBase.Stickmen.SMaschine;
using CodeBase.UI.WorldSpaceCanvas;
using DG.Tweening;
using UnityEngine;

namespace CodeBase.Car
{
    public class Car : MonoBehaviour
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private HealthBarVisual healthBarVisual;
        [SerializeField] private Material material;
        [SerializeField] private int health = 100;
        
        private HealthService _healthService;
        private float _doColorDuration = 0.1f;

        public Action OnDeath;

        private void Start()
        {
            ResetColor();
            _healthService = new HealthService(health);
            healthBarVisual.ActivateHealthBar(_healthService.GetCurrentHealth());

            _healthService.OnDeath += Death;
            triggerObserver.TriggerEnter += TriggerEnter;
        }

        private void Death()
        {
            OnDeath?.Invoke();
        }

        private void TriggerEnter(Collider collider)
        {
            if (collider.GetComponentInParent<Enemy.Enemy>() is { } enemy)
            {
                _healthService.Decrease(enemy.GetDamage());
                healthBarVisual.ActivateHealthBar(_healthService.GetCurrentHealth());
                material.DOColor(Color.red, _doColorDuration).OnComplete(ResetColor);
            }
        }

        private void ResetColor()
        {
            material.DOColor(Color.white, _doColorDuration);
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
            _healthService.OnDeath -= Death;
        }
    }
}