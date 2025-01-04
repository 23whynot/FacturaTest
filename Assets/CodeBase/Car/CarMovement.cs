using CodeBase.UI.Panels;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace CodeBase.Car
{
    public class CarMovement : MonoBehaviour
    {
        [Header("Movement Parameters")] 
        [SerializeField] private float speed = 7f;
        [SerializeField] private float wobbleAmount = 2f;
        [SerializeField] private float wobbleDuration = 6f;
        [SerializeField] private float rotateAngle = 2f;
        
        [Header("Visual parameters")]
        [SerializeField] private ParticleSystem particle;

        private Tween _wobbleTween;
        private Tween _moveTween;
        private Tween _rotateTween;
        private MainMenu _mainMenu;


        [Inject]
        public void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        private void Start()
        {
            particle.Stop();
            _mainMenu.OnStartGame += StartMovement;
        }

        public float GetSpeed()
        {
            return speed;
        }

        private void StartMovement()
        {
            StartMove();
            particle.Play();
        }

        private void StartMove()
        {
            float moveDistance = 1000f;

            _moveTween = transform.DOMoveZ(transform.position.z - moveDistance, speed)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);

            _wobbleTween = transform
                .DOMoveX(wobbleAmount, wobbleDuration)
                .SetRelative()
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);

            _rotateTween = transform
                .DORotate(new Vector3(0, -rotateAngle, 0), wobbleDuration / 2f)
                .From(new Vector3(0, rotateAngle, 0))
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }


        private void StopMovement()
        {
            if (_moveTween != null && _moveTween.IsActive())
            {
                _moveTween.Kill();
            }

            if (_wobbleTween != null && _wobbleTween.IsActive())
            {
                _wobbleTween.Kill();
            }

            if (_rotateTween != null && _rotateTween.IsActive())
            {
                _rotateTween.Kill();
            }
        }

        private void OnDestroy()
        {
            _mainMenu.OnStartGame -= StartMovement;
            StopMovement();
        }
    }
}