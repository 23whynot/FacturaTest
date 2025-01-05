using UnityEngine;

namespace CodeBase.Camera
{
    public class LookAtCamera : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 50f;
        private void LateUpdate()
        {
            Vector3 direction = UnityEngine.Camera.main.transform.position - transform.position;
            direction.y = 0;
        
            Quaternion targetRotation = Quaternion.LookRotation(-direction);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}