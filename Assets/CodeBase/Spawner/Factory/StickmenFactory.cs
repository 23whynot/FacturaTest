using CodeBase.Constants;
using CodeBase.Core.ObjectPool;
using CodeBase.Stickmen;
using UnityEngine;
using Zenject;

namespace CodeBase.Spawner.Factory
{
    public class StickmenFactory
    {
        private GameObject _stickmanPrefab;

        private ObjectPool _pool;
        private SpawnController _spawnController;

        [Inject]
        public void Construct(ObjectPool pool, SpawnController spawnController)
        {
            _pool = pool;
            _spawnController = spawnController;
        }

        public void StartFactory()
        {
            _stickmanPrefab = Resources.Load<GameObject>("Stickman/" + ResourcesConstants.StickmanPrefab);
            

            _pool.RegisterPrefab<Stickman>(_stickmanPrefab, _spawnController.GetPreLoadCount());
        }

        public void SpawnStickman()
        {
            Spawn<Stickman>();
        }

        private T Spawn<T>() where T : Stickman
        {
            T stickmen = _pool.GetObject<T>();
            stickmen.transform.position = _spawnController.GetSpawnPoint(); //TODO
            Debug.Log("Spawned Stickman: " + stickmen.transform.position);
            stickmen.Init(_spawnController);
            stickmen.Activate();

            return stickmen;
        }
    }
}