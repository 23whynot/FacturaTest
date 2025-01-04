using System;
using CodeBase.Core.ObjectPool;
using CodeBase.Spawner;
using UnityEngine;
using Zenject;

namespace CodeBase.Environment
{
    public class Ground : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private Collider boxCollider;
        [SerializeField] private Collider groundCollider;

        private GroundSpawner _groundSpawner;
        
        [Inject]
        public void Construct(GroundSpawner groundSpawner)
        {
            _groundSpawner = groundSpawner;
        }

        public bool IsActive { get; private set; }

        public void Activate()
        {
            DeactivateCollider();
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }

        public void ActivateCollider()
        {
            boxCollider.enabled = true;
        }

        private void DeactivateCollider()
        {
            boxCollider.enabled = false;
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponentInParent<Car.Car>() is { } car)
            {
                Deactivate();
                _groundSpawner.GroundDeactivated();
            }
        }
    }
}