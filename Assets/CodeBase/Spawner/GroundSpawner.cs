using CodeBase.Core.ObjectPool;
using CodeBase.Environment;
using CodeBase.Level;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawner
{
    public class GroundSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject groundPrefab;
        [SerializeField] private Vector3 spawnStartPoint;
        [SerializeField] private Vector3 spawnOffset;
        private int _timeToEndOneGround = 15;
        private int _preLoadCount = 5;
        private int _spawnCount;

        private ObjectPool _pool;
        private LevelManager _levelManager;

        [Inject]
        public void Construct(ObjectPool pool, LevelManager levelManager)
        {
            _pool = pool;
            _levelManager = levelManager;
        }

        private void Awake()
        {
            _pool.RegisterPrefab<Ground>(groundPrefab, _preLoadCount);
        }

        private void Start()
        {
            SpawnGround();
        }

        private void SpawnGround()
        {
            Vector3 currentPosition = spawnStartPoint;

            _spawnCount = Mathf.CeilToInt(_levelManager.GetLevelTime() / _timeToEndOneGround);

            for (int i = 0; i < _spawnCount; i++)
            {
                Ground ground = _pool.GetObject<Ground>();
                ground.transform.position = currentPosition;
                ground.Activate();
                currentPosition += spawnOffset;
            }
        }
    }
}
