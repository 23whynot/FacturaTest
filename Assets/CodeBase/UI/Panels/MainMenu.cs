using System;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.Panels
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        
        public Action OnStartGame;
        
        private void Awake()
        {
            playButton.onClick.AddListener(PlayClick);
        }

        private void PlayClick()
        {
            OnStartGame?.Invoke();
        }

        private void OnDisable()
        {
            playButton.onClick.RemoveAllListeners();
        }
    }
}
