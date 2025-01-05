using System.Collections;
using CodeBase.Bullets;
using CodeBase.Camera;
using CodeBase.Constants;
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

        [Inject]
        private void Construct(ObjectPool objectPool, CameraController cameraController, AudioService audioService)
        {
            _objectPool = objectPool;
            _cameraController = cameraController;
            _audioService = audioService;
        }

        private AudioService _audioService;
        private TurretModel _model;
        private ObjectPool _objectPool;
        private CameraController _cameraController;
        private Coroutine _coroutine;
        private Vector2 _startTouchPosition;
        private Vector2 _currentTouchPosition;
        private bool _isSwiping;
        private bool _isFiring;

        private void Awake()
        {
            _model = new TurretModel(-360, 360f);
            
            _model.CurrentAngle = transform.localEulerAngles.y;
            UpdateTurretRotation(_model.CurrentAngle);
            
            _objectPool.RegisterPrefab<Bullet>(bulletPrefab, preLoadCount);
        }

        private void Start()
        {
            _cameraController.OnCameraReady += StartSpawnCoroutine;
        }

        private void Update()
        {
            if (_isFiring)
            {
                HandleSwipe();
            }
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
            _isFiring = true;
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
                Bullet bullet = _objectPool.GetObject<Bullet>();
                bullet.transform.position = firePoint.position;
                bullet.transform.rotation = firePoint.rotation;
                bullet.Activate();
                _audioService.PlaySound(AudioConstants.Shoot);
                yield return new WaitForSeconds(fireRate);
            }
        }

        private void HandleSwipe()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        _startTouchPosition = touch.position;
                        _isSwiping = true;
                        break;

                    case TouchPhase.Moved:
                        if (_isSwiping)
                        {
                            _currentTouchPosition = touch.position;
                            AdjustAim();
                        }
                        break;

                    case TouchPhase.Ended:
                    case TouchPhase.Canceled:
                        _isSwiping = false;
                        break;
                }
            }

            if (Input.GetMouseButtonDown(0))
            {
                _startTouchPosition = Input.mousePosition;
                _isSwiping = true;
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
            UpdateTurretRotation(_model.CurrentAngle);

            _startTouchPosition = _currentTouchPosition;
        }
        
        private void UpdateTurretRotation(float angle)
        {
            if (transform != null)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
            }
        }

        private void OnDestroy()
        {
            _cameraController.OnCameraReady -= StartSpawnCoroutine;
            StopSpawnCoroutine();
        }
    }
}
