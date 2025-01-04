using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI.Panels
{
    public class LooseMenu : MonoBehaviour
    {
        [SerializeField] private Button restartGameButton;

        private void Awake()
        {
            restartGameButton.onClick.AddListener(RestartGame);
        }

        private void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            DOTween.KillAll();
        }

        private void OnDestroy()
        {
            restartGameButton.onClick.RemoveAllListeners();
        }
    }
}
