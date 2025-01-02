using System.Collections;
using CodeBase.Bullets;
using CodeBase.Core.ObjectPool;
using CodeBase.UI.Panels;
using UnityEngine;

using Zenject;

namespace CodeBase.Turret
{
    public class TurretController : MonoBehaviour
    {
        [Header("Swipe Settings")]
        [SerializeField] private float sensitivity = 0.2f;
        
        [Header("Turret Settings")]
        [SerializeField] private float fireRate = 0.3f;
        
        [Header("Bullet")]
        [SerializeField] private Transform firePoint;
        [SerializeField] private Transform targetForBullet;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private int preLoadCount = 10;

        private TurretModel _model;
        [SerializeField] private TurretView turretView;

        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;
        private bool _isSwiping;
        private Coroutine _coroutine;
        
        

        private ObjectPool _objectPool;
        private MainMenu _mainMenu;
        
        [Inject]
        private void Construct(ObjectPool objectPool, MainMenu mainMenu)
        {
            _objectPool = objectPool;
            _mainMenu = mainMenu;
        }

        private void Awake()
        {
            _model = new TurretModel(-360, 360f); // Углы ограничения TODO
            
            _objectPool.RegisterPrefab<Bullet>(bulletPrefab, preLoadCount);
        }

        private void Start()
        {
            turretView.ShowSwipeHint(true);
            _mainMenu.OnStartGame += StartSpawnCoroutine;
        }

        private void Update()
        {
            HandleSwipe();
        }

        public Transform GetTargetForBullet()
        {
            return targetForBullet;
        }

        public Transform GetFirePoint()
        {
            return firePoint;
        }

        private void StartSpawnCoroutine()
        {
            _coroutine = StartCoroutine(SpawnCoroutine());
        }

        private void StopSpawnCoroutine()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }
        }

        private IEnumerator SpawnCoroutine()
        {
            while (true)
            {
                Bullet  bullet = _objectPool.GetObject<Bullet>();
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.Activate();
                yield return new WaitForSeconds(fireRate);
            }
        }


        private void HandleSwipe()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _isSwiping = true;
                turretView.ShowSwipeHint(false);
            }

            if (Input.GetMouseButton(0) && _isSwiping)
            {
                _currentTouchPosition = Input.mousePosition;
                AdjustAim();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isSwiping = false;
            }
        }

        private void AdjustAim()
        {
            float swipeDeltaX = (_currentTouchPosition.x - _startTouchPosition.x) * sensitivity;

            _model.CurrentAngle = _model.ClampAngle(_model.CurrentAngle + swipeDeltaX);
            turretView.UpdateTurretRotation(_model.CurrentAngle);

            _startTouchPosition = _currentTouchPosition;
        }

        private void OnDisable()
        {
            _mainMenu.OnStartGame -= StartSpawnCoroutine;
            StopSpawnCoroutine();
        }
    }
}