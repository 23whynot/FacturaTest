using System;
using System.Collections;
using CodeBase.Core.ObjectPool;
using CodeBase.Environment;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawner
{
    public class GroundSpawner : MonoBehaviour
    {
        [Header("Prefabs")] 
        [SerializeField] private GameObject groundPrefab;

        [Header("Configuration")] [SerializeField]
        private int spawnCount = 4;

        [SerializeField] private int maxSegmentInMoment = 2;

        [SerializeField] private Vector3 spawnStartPoint;
        [SerializeField] private Vector3 spawnOffset;


        private int _groundActivated;
        private int _preLoadCount = 2;
        private int _groundSpawnedOnLevel;
        private ObjectPool _pool;
        private Coroutine _spawnRoutine;

        [Inject]
        public void Construct(ObjectPool pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _pool.RegisterPrefab<Ground>(groundPrefab, _preLoadCount);
        }

        private void Start()
        {
            StartSpawnGround();
        }

        public void GroundDeactivated()
        {
            _groundActivated--;
        }

        private void StartSpawnGround()
        {
            _spawnRoutine = StartCoroutine(SpawnGroundCoroutine());
        }

        private IEnumerator SpawnGroundCoroutine()
        {
            Vector3 currentPosition = spawnStartPoint;

            while (_groundSpawnedOnLevel < spawnCount)
            {
                if (_groundActivated < maxSegmentInMoment)
                {
                    Ground ground = _pool.GetObject<Ground>();
                    ground.transform.position = currentPosition;
                    ground.Activate();
                    currentPosition += spawnOffset;
                    
                    _groundSpawnedOnLevel++;
                    _groundActivated++;
                    
                    if (_groundSpawnedOnLevel == spawnCount)
                    {
                        ground.ActivateCollider();
                    }
                }
                yield return null;
            }
        }

        private void StopSpawnGround()
        {
            if (_spawnRoutine != null)
            {
                StopCoroutine(_spawnRoutine);
                _spawnRoutine = null;
            }
        }

        private void OnDestroy()
        {
            StopSpawnGround();
        }
    }
}