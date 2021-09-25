
using UnityEngine;

namespace  TopDownShooter.Combat{

    public class Health : MonoBehaviour{

        public float startingHealth;
        float currentHealth;
        public bool IsDead{get;private set;}
        private void Start() {
            currentHealth = startingHealth;
        }
        public void GetDamage(float damage){
            currentHealth = Mathf.Max(0 , currentHealth - damage);
            if (currentHealth == 0){
                Die();
            }
        }

        public void Die(){
            IsDead = true;
            Destroy(gameObject);
            print("Dead");
        }





    }

}