

using UnityEngine;

namespace  TopDownShooter.Combat{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour{
        public float autoDestroyTime = 0.5f;
        float damage;
        private void OnEnable() {
            Destroy(gameObject, autoDestroyTime);
            
        }
        private void Update() {
            
        }

        private void OnTriggerEnter(Collider other) {
            Health target = other.GetComponent<Health>();
            if (target == null){
                Destroy(gameObject);
                return;
            } 
            target.GetDamage(damage);
            Destroy(gameObject);

        }
        

        public void InitializeBullet(Vector3 force , float damage){
            this.damage = damage;
            GetComponent<Rigidbody>().velocity = force;
            
        }

        
    }
}