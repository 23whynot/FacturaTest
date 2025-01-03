using UnityEngine;

namespace CodeBase.Turret
{
    public class TurretView : MonoBehaviour
    {
        public void UpdateTurretRotation(float angle)
        {
            if (transform != null)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
            }
        }
        
    }
}