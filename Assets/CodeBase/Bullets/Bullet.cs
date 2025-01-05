using CodeBase.Car;
using CodeBase.Core.ObjectPool;
using CodeBase.Turret;
using DG.Tweening;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace CodeBase.Bullets
{
    public class Bullet : MonoBehaviour, IPoolableObject
    {
        [SerializeField] private int minDamage = 0;
        [SerializeField] private int maxDamage = 99;
        [SerializeField] private BulletTracer tracer;

        [Inject]
        private void Construct(TurretController turretController, CarMovement carMovement)
        {
            _turretController = turretController;
            _carMovement = carMovement;
            
            _isInjected = true;
            
            tracer.Init(_turretController.GetFirePoint());
        }

        private TurretController _turretController;
        private CarMovement _carMovement;
        private Tween _moveTween;
        readonly float _moveDurationMultiplayer = 0.1f;
        private bool _isInjected;
        public bool IsActive { get; private set; }


        public int GetDamage()
        {
            return Random.Range(minDamage, maxDamage);
        }

        public void Activate()
        {
            if (_isInjected)
            {
                tracer.ShowTracer();
            }
            gameObject.SetActive(true);
            IsActive = true;
            StartMove();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
            IsActive = false;
            StopMove();
        }

        private void StartMove()
        {
            _moveTween = transform.DOMove(_turretController.GetTargetForBullet().position,
                    _carMovement.GetSpeed() * _moveDurationMultiplayer).SetEase(Ease.Linear)
                .OnComplete(Deactivate);
        }

        private void StopMove()
        {
            if (_moveTween != null && _moveTween.IsActive())
            {
                _moveTween.Kill();
            }
        }
    }
}