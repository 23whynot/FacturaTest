using CodeBase.Constants;
using CodeBase.Core.ObjectPool;
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
        public void Init()
        {
            _stickmanPrefab = Resources.Load<GameObject>("Stickman/" + ResourcesConstants.StickmanPrefab);
            
            _pool.RegisterPrefab<Enemy.Enemy>(_stickmanPrefab, _spawnController.GetPreLoadCount());
        }
        public void SpawnStickman()
        {
            Spawn<Enemy.Enemy>();
        }
        private T Spawn<T>() where T : Enemy.Enemy
        {
            T stickmen = _pool.GetObject<T>();
            stickmen.transform.position = _spawnController.GetSpawnPoint();
            stickmen.Init(_spawnController);
            stickmen.Activate();
            
            return stickmen;
        }
    }
}