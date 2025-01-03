using System;
using CodeBase.Stickmen;

namespace CodeBase.Services
{
    public class HealthService
    {
        private int _currentHealth;

        public event Action<int> OnHealthChanged; // FOR UI
        public event Action OnDeath; //FOR GAME

        public HealthService(int health)
        {
            _currentHealth = health;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }

        public void Decrease(int amount)
        {
            _currentHealth -= amount;
            OnHealthChanged?.Invoke(_currentHealth);

            if (_currentHealth == 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}

