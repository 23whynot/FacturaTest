using System;
using CodeBase.Constants;
using CodeBase.UI.Panels;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace CodeBase.Camera
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        [Inject]
        public void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }

        private MainMenu _mainMenu;
        private Coroutine _coroutine;
        private Tween _moveTween;
        public Action OnCameraReady;

        private void Start()
        {
            _mainMenu.OnStartGame += AnimationStart;
        }

        private void AnimationStart()
        {
            animator.SetBool(AnimationConstants.StartGame, false);
        }

        private void OnAnimationEnd()
        {
            OnCameraReady?.Invoke();
        }
        
        private void OnDestroy()
        {
            _mainMenu.OnStartGame -= AnimationStart;
        }
    }
}