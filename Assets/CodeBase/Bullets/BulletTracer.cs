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
            if (firePoint == null)
            {
                Debug.LogError("End point is null!");
                return;
            }

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
            if (_isInitialized)
            {
                lineRenderer.SetPosition(0, _startPointPosition);
                lineRenderer.SetPosition(1, bulletTransform.position);
            }
        }
    }
}