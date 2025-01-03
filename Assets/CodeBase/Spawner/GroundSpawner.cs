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
        [SerializeField] private GameObject groundWitchColliderPrefab;

        [Header("Configuration")] 
        [SerializeField] private int spawnCount = 4;

        [SerializeField] private Vector3 spawnStartPoint;
        [SerializeField] private Vector3 spawnOffset;


        private int _preLoadCount = 2;
        private ObjectPool _pool;

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
            SpawnGround();
        }

        private void SpawnGround()
        {
            Vector3 currentPosition = spawnStartPoint;

            for (int i = 0; i < spawnCount - 1; i++)
            {
                Ground ground = _pool.GetObject<Ground>();
                ground.transform.position = currentPosition;
                ground.Activate();
                currentPosition += spawnOffset;
            }

            GameObject groundWitchCollider = Instantiate(groundPrefab);
            groundWitchCollider.transform.position = currentPosition;
        }
    }
}