using System;
using CodeBase.Bullets;
using CodeBase.Car;
using CodeBase.Core.ObjectPool;
using CodeBase.Spawner;
using CodeBase.UI.WorldSpaceCanvas;
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
        [SerializeField] private int scoreCountMax = 20;
        [SerializeField] private int scoreCountMin = 5;


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

        public int GetScoreCount()
        {
            return Random.Range(scoreCountMin, scoreCountMax);
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
            else if (other.GetComponentInParent<CarMovement>() is {} car)
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
