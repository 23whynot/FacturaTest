using System;

namespace CodeBase.Services
{
    public class HealthService
    {
        private int _currentHealth;
        
        public event Action OnDeath;

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
            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}

