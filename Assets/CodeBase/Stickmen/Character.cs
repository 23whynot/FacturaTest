using UnityEngine;

namespace CodeBase.Stickmen
{
    [CreateAssetMenu(fileName = "New Enemy Character", menuName = "Swipe Objects/Character")]
    public class Character : ScriptableObject
    {
        public int Health;
        public float Duration;
        public int ScoreCost;
    }
}                   