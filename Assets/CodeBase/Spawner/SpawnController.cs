using System.Collections;
using CodeBase.Spawner.Factory;
using CodeBase.UI.Panels;
using UnityEngine;
using Zenject;
namespace CodeBase.Spawner
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private SpawnArea spawnArea;
        [SerializeField] private int preLoadCount = 10;
        [SerializeField] private int countEnemy = 100;
        [SerializeField] private int enemyInMoment = 15;
        
        private StickmenFactory _factory;
        private MainMenu _mainMenu;
        private Coroutine _spawnCoroutine;
        private int _stickmenSpawned;
        private int _stickmenSpawnedOnLevel;

        [Inject]
        public void Construct(StickmenFactory stickmenFactory, MainMenu mainMenu)
        {
            _factory = stickmenFactory;
            _mainMenu = mainMenu;
        }

        private void Start()
        {
            _factory.Init();
            _mainMenu.OnStartGame += StartSpawnCoroutine;
        }
        public void EnemyDespawn()
        {
            _stickmenSpawned--;
        }
        public int GetPreLoadCount()
        {
            return preLoadCount;
        }
        public Vector3 GetSpawnPoint()
        {
            return spawnArea.GetSpawnPoint();
        }
        public Transform GetTargetTransform()
        {
            return target;
        }
        private void StartSpawnCoroutine()
        {
            _spawnCoroutine = StartCoroutine(SpawnProcess());
        }
        private IEnumerator SpawnProcess()
        {
            while (true)
            {
                if (_stickmenSpawned < enemyInMoment & _stickmenSpawnedOnLevel < countEnemy)
                {
                    _factory.SpawnStickman();
                    _stickmenSpawned++;
                    _stickmenSpawnedOnLevel++;
                }
                
                yield return null;
            }
        }
        
        private void OnDisable()
        {
            if (_spawnCoroutine != null)
            {
                StopCoroutine(_spawnCoroutine);
                _spawnCoroutine = null;
            }
            _mainMenu.OnStartGame -= StartSpawnCoroutine;
        }
    }
}