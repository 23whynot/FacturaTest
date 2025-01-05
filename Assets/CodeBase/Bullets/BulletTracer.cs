using UnityEngine;

namespace CodeBase.Bullets
{
    [RequireComponent(typeof(LineRenderer))]
    public class BulletTracer : MonoBehaviour
    {
        [SerializeField] private Transform bulletTransform;
        [SerializeField] private LineRenderer lineRenderer;

        private Transform _firePoint;
        private bool _isInitialized;
        private Vector3 _startPointPosition;

        public void Init(Transform firePoint)
        {
            _firePoint = firePoint;
            _isInitialized = true;
        }

        private void OnEnable()
        {
            if (_isInitialized)
            {
                _startPointPosition = _firePoint.position;
            }
        }

        private void Update()
        {
            ShowTracer();
        }

        public void ShowTracer()
        {
            if (!_isInitialized) return;
            lineRenderer.SetPosition(0, _startPointPosition);
            lineRenderer.SetPosition(1, bulletTransform.position);
        }
    }
}