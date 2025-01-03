using CodeBase.Spawner.Factory;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawner
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private SpawnArea spawnAreaOnStartGround;
        [SerializeField] private Transform target;
        [SerializeField] private GroundSpawner groundSpawner;
        [SerializeField] private int preLoadCount = 10;
        [SerializeField] private int countStickmen = 30;

        private StickmenFactory _factory;
        private int _enemyOnOneGround;
        private int _groundWitchStart;

        [Inject]
        public void Construct(StickmenFactory stickmenFactory)
        {
            _factory = stickmenFactory;
        }

        private Coroutine _spawnCoroutine;
        
        private int _stickmenSpawned;

        private void Awake()
        {
            _groundWitchStart = groundSpawner.GetSpawnCount() + 1;
            _enemyOnOneGround = (countStickmen + _groundWitchStart / 2) / _groundWitchStart;
        }

        private void Start()
        {
            _factory.StartFactory();
            StartSpawn(spawnAreaOnStartGround);
            groundSpawner.OnSpawn += StartSpawn;
        }

        public int GetPreLoadCount()
        {
            return preLoadCount;
        }

        public Transform GetTargetTransform()
        {
            return target;
        }

        private void StartSpawn(SpawnArea spawnArea)
        {
            for (int i = 0; i < _enemyOnOneGround; i++)
            {
                _factory.SpawnStickman(spawnArea);
            }
        }

        private void OnDestroy()
        {
            groundSpawner.OnSpawn -= StartSpawn;
        }
    }
}