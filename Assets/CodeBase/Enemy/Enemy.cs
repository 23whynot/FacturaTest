using System;
using CodeBase.Bullets;
using CodeBase.Car;
using CodeBase.Core.ObjectPool;
using CodeBase.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.Enemy
{
    public class Enemy : MonoBehaviour, IPoolableObject
    {
        [Header("Enemy Parameters")]
        [SerializeField] private float speed = 6;
        [SerializeField] private int health = 100;
        [SerializeField] private int minDamage = 5;
        [SerializeField] private int maxDamage = 25;


        [Header("Scripts")]
        [SerializeField] private TriggerObserver triggerObserver;
        [SerializeField] private EnemyBehavior enemyBehavior;
        
        
        private SpawnController _spawnController;
        public bool IsActive { get; private set; }


        public Action<int> OnBullet;

        public void Init(SpawnController spawnController) 
        {
            _spawnController = spawnController;
            
            enemyBehavior.Init(_spawnController);
            
        }

        private void Start()
        {
            triggerObserver.TriggerEnter += TriggerEnter;
        }

        public float GetSpeed()
        {
            return speed;
        }

        public int GetHealth()
        {
            return health;
        }

        public int GetDamage()
        {
            return Random.Range(minDamage, maxDamage);
        }

        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
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
            enemyBehavior.Death();
        }

        private void OnDestroy()
        {
            triggerObserver.TriggerEnter -= TriggerEnter;
        }
    }
}
