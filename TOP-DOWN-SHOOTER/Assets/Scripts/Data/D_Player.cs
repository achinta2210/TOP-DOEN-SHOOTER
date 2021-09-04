using UnityEngine;

namespace TopDownShooter.Data
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Data/new Player Data", order = 0)]
    public class D_Player : ScriptableObject
    {
        public float movementSpeed = 10f;
        public float minMouseDistance = 0.1f;
        [Range(0.0f , 5.0f)]
        public float lookAhedAmmount = 1.7f;
    }
}