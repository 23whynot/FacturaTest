using UnityEngine;

namespace CodeBase.Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private int levelTime;

        public int GetLevelTime()
        {
            return levelTime;
        }

    }
}
