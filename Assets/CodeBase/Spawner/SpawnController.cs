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
        [SerializeField] private int countStickmen = 100;

        private StickmenFactory _factory;
        private MainMenu _mainMenu;

        [Inject]
        public void Construct(StickmenFactory stickmenFactory, MainMenu mainMenu)
        {
            _factory = stickmenFactory;
            _mainMenu = mainMenu;
        }

        private Coroutine _spawnCoroutine;
        
        private int _stickmenSpawned;

        private void Start()
        {
            _factory.StartFactory();

            _mainMenu.OnStartGame += StartSpawnCoroutine;
        }

        public void StickmanDespawned()
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
                if (countStickmen > _stickmenSpawned)
                {
                    _factory.SpawnStickman();
                    _stickmenSpawned++;
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