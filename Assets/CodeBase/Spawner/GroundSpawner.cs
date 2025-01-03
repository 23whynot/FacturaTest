using System;
using CodeBase.Core.ObjectPool;
using CodeBase.Environment;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawner
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject groundPrefab;
        [SerializeField] private GameObject groundWitchWinColliderPrefab;
        [SerializeField] private Vector3 spawnStartPoint;
        [SerializeField] private Vector3 spawnOffset;
        
        private int _spawnCount = 3;
        public Action<SpawnArea> OnSpawn;
        
        private ObjectPool _pool;

        [Inject]
        public void Construct(ObjectPool pool)
        {
            _pool = pool;
        }

        private void Awake()
        {
            _pool.RegisterPrefab<Ground>(groundPrefab, _spawnCount);
        }

        private void Start()
        {
            SpawnGround();
        }

        public int GetSpawnCount()
        {
            return _spawnCount;
        }

        private void SpawnGround()
        {
            Vector3 currentPosition = spawnStartPoint;
            for (int i = 0; i < _spawnCount-1; i++)
            {
               
                Ground ground = _pool.GetObject<Ground>();
                ground.transform.position = currentPosition;
                ground.Activate();
                currentPosition += spawnOffset;
                if (ground.TryGetComponent<SpawnArea>(out SpawnArea spawnArea))
                {
                    OnSpawn?.Invoke(spawnArea);
                }
            }
            GameObject groundWitchWinCollider = Instantiate(groundWitchWinColliderPrefab);
            groundWitchWinCollider.transform.position = currentPosition;
            if (groundWitchWinCollider.TryGetComponent<SpawnArea>(out SpawnArea spawnAreaOnGroundWitchCollider))
            {
                OnSpawn?.Invoke(spawnAreaOnGroundWitchCollider);
            }
        }
    }
}
