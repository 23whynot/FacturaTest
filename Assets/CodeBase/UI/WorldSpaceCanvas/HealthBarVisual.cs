using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.UI.WorldSpaceCanvas
{
    public class HealthBarVisual : MonoBehaviour
    {
        [Header("UI Elements")] 
        [SerializeField] private Image healthBar;
        [SerializeField] private GameObject fillBackGround;
        
        private float _currentHealth = 1f;
        
        public void ActivateHealthBar(int health)
        {
            fillBackGround.SetActive(true);
            healthBar.fillAmount = 0f;       
            _currentHealth = 1f;     
            SetCount(health); 
        }

        public void SetCount(int count)
        {
            _currentHealth = count / 100f;
            healthBar.fillAmount = _currentHealth;
        }

        public void DeactivateHealthBar()
        {
            fillBackGround.SetActive(false); // Скрываем фон полоски здоровья
              
        }
    }
}
