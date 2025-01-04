using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.UI.WorldSpaceCanvas
{
    public class ScoreDisplayManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private Canvas worldSpaceCanvas;
        [SerializeField] private float fadeDuration = 0.3f;

        public void ActivateScoreDisplay(int score)
        {
            worldSpaceCanvas.enabled = true;
            ShowScoreFromDeath(score);
        }
        private void ShowScoreFromDeath(int score)
        {
            scoreText.text = score.ToString();
            
            scoreText.DOFade(1f, fadeDuration / 2).OnComplete(() =>
            {
                
                scoreText.DOFade(0f, fadeDuration / 2).OnComplete(() =>
                {
                    scoreText.text = "";
                });
            });
        }

        public void DeactivateScoreDisplay()
        {
            worldSpaceCanvas.enabled = false;
        }
    }
}
