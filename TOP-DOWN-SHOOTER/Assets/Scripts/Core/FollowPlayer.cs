using UnityEngine;

namespace TopDownShooter.Core{

    public class FollowPlayer : MonoBehaviour{

        public Transform target;

        private void LateUpdate() {
            if (target != null) transform.position = target.position;
        }

    }

}