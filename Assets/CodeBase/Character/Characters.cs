using System;
using CodeBase.Bullets;
using CodeBase.Car;
using CodeBase.Core.ObjectPool;
using CodeBase.Spawner;
using CodeBase.Stickmen;
using UnityEngine;


namespace CodeBase.Character
{
    public class Characters : MonoBehaviour, IPoolableObject
    {
        [Header("Character Parameters")]
        [SerializeField] private int health = 100;
        [SerializeField] private float durationToTarget = 3;
        
        [Header("Scripts")]
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private CharacterBehavior characterBehavior;
        
        
        private SpawnController _spawnController;
        public bool IsActive { get; private set; }


        public Action<int> OnBullet;

        public void Init(SpawnController spawnController) 
        {
            _spawnController = spawnController;
            
            characterBehavior.Init(_spawnController);
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

        public int GetHealth()
        {
            return health;
        }

        public float GetDurationToTarget()
        {
            return durationToTarget;
        }

        public void Deactivate()
        {
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
            characterBehavior.Death();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
        }
    }
}
