
using UnityEngine;
using TopDownShooter.InputHandler;
namespace TopDownShooter.Combat{
    
    public class Shooter : MonoBehaviour {
        public Weapon startingWeapon = null;
        public Material destroyableMat;
        Weapon currentWeapon;
        Material lineMat;
        Health target;
        float timeSinceStartedShooting = 0.0f;
        InputManager input;
        Vector3 mousePos ;
        private void Start() {
            currentWeapon = startingWeapon;
            timeSinceStartedShooting = Mathf.Infinity;
            input = GetComponent<InputManager>();
        }
        private void Update() {
           if (input.Reload){
               currentWeapon.Reload(30);
           }
        }
        public void ShootOnce(){
            if (currentWeapon == null) return;
            if (currentWeapon.constrains.isReloading) return;
            if (input == null) {
                mousePos = new Vector3(target.transform.position.x , transform.position.y , target.transform.position.y);
            }else{
                mousePos = input.MousePosition;
            }
            if (currentWeapon.currentBulletCount == 0) return;
            currentWeapon.ShootBullet(1 );
            currentWeapon.ShootBulletPrefab(mousePos);
            if (currentWeapon.data.projectilePrefab != null) return;
            target = currentWeapon.Shoot(mousePos);
            currentWeapon.DrawBulletLine();
            if (target == null) return;
            target.GetDamage(currentWeapon.data.startingDamage);

        }

     
        
        public void AutometicShoot(bool canShoot){
            if (canShoot){
                if (timeSinceStartedShooting >= currentWeapon.data.fireRate){
                    ShootOnce();
                    timeSinceStartedShooting = 0.0f;
                }
                else{
                    timeSinceStartedShooting += Time.deltaTime;
                }
            }else{
                timeSinceStartedShooting = Mathf.Infinity;
            }
            
        }
        
        
    }
}