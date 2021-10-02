using UnityEngine;

namespace TopDownShooter.Combat{

    public class Effect : MonoBehaviour{
        public float autoDeastroyTime = 1f;

        private void Start() {
            Destroy(gameObject , autoDeastroyTime);
        }
    }
}