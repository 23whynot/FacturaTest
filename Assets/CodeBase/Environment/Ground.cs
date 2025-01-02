using CodeBase.Core.ObjectPool;
using UnityEngine;

namespace CodeBase.Environment
{
    public class Ground : MonoBehaviour, IPoolableObject
    {
        public bool IsActive { get; private set;  }
        public void Activate()
        {
            IsActive = true;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            IsActive = false;
            gameObject.SetActive(false);
        }
    }
}
