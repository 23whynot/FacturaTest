using UnityEngine;

namespace CodeBase.Turret
{
    public class TurretModel
    {
        public float CurrentAngle { get; set; }
        private readonly float _minAngle;
        private readonly float _maxAngle;

        public TurretModel(float minAngle, float maxAngle)
        {
            _minAngle = minAngle;
            _maxAngle = maxAngle;
            
            CurrentAngle = 0f;
        }

        public float ClampAngle(float angle)
        {
            return Mathf.Clamp(angle, _minAngle, _maxAngle);
        }
    }
}