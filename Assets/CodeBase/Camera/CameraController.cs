using System;
using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] private float animationDuration;
        
        private MainMenu _mainMenu;
        private Coroutine _coroutine;
        private Tween _moveTween;


        [Inject]
        public void Construct(MainMenu mainMenu)
        {
            _mainMenu = mainMenu;
        }
        
        public Action OnCameraReady;

        private void Start()
        {
            _mainMenu.OnStartGame += AnimationStart;
        }

        private void AnimationStart()
        {
            animator.SetBool(AnimationConstans.StartGame, false);
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
