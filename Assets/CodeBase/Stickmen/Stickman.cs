using System;
using CodeBase.Bullets;
using CodeBase.Car;
using CodeBase.Core.ObjectPool;
using CodeBase.Spawner;
using UnityEngine;


namespace CodeBase.Stickmen
{
    public class Stickman : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private StickmanBehavior stickmanBehavior;
        
        
        private SpawnController _spawnController;
        public bool IsActive { get; private set; }
        private bool _isInitialized;

        public Action<int> OnBullet;

        public void Init(SpawnController spawnController) 
        {
            _spawnController = spawnController;
            
            stickmanBehavior.Init(_spawnController);
            
            _isInitialized = true;
        }

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            if (_isInitialized)
            {
                _spawnController.StickmanDespawned();
            }
            IsActive = false;
            gameObject.SetActive(false);
        }

        private void TriggerEnter(Collider other)
        {
            
            if (other.TryGetComponent<Bullet> (out var bullet))
            {
                OnBullet?.Invoke(bullet.GetDamage());
            }
            else if (other.TryGetComponent<CarMovement> (out var car))
            {
                Despawn();
            }
            else
            {
                Despawn();
            }
        }

        private void Despawn()
        {
            stickmanBehavior.Death();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
        }
    }
}
