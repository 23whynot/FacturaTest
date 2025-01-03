using CodeBase.UI.Panels;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Car
{
    public class CarMovement : MonoBehaviour
    {
        [Header("Movement Parameters")] 
        [SerializeField] private float moveDuration;

        [Header("Wobble Parameters")] 
        [SerializeField] private float wobbleAmount;
        [SerializeField] private float wobbleDuration;

        private MainMenu _mainMenu;

        [Inject]
        public void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        private Tween _wobbleTween;
        private Tween _moveTween;

        private void Start()
        {
            _mainMenu.OnStartGame += StartMovement;
        }

        public float GetMoveDuration()
        {
            return moveDuration;
        }

        private void StartMovement()
        {
            StartMove();
            StartWobble();
        }

        private void StartMove()
        {
            float moveDistance = 1000f;

            _moveTween = transform.DOMoveZ(transform.position.z - moveDistance, moveDuration)
                .SetSpeedBased(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        private void StartWobble()
        {
            _wobbleTween = transform
                .DOMoveX(wobbleAmount, wobbleDuration)
                .SetRelative()
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
        }

        private void OnDestroy()
        {
            _mainMenu.OnStartGame -= StartMovement;
        }
    }
}