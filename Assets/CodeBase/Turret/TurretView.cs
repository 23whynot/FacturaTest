using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.Turret
{
    public class TurretView : MonoBehaviour
    {
        public Text swipeHintText; //TODO

        public void UpdateTurretRotation(float angle)
        {
            if (transform != null)
            {
                transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, angle, transform.localEulerAngles.z);
            }
        }

        public void ShowSwipeHint(bool show)
        {
            if (swipeHintText != null)
            {
                //   swipeHintText.gameObject.SetActive(show);
            }
        }
        
    }
}