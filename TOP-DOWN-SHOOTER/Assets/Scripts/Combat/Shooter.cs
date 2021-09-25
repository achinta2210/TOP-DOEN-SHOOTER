
using System.Collections;
using UnityEngine;
namespace TopDownShooter.Combat{
    
    public class Shooter : MonoBehaviour {
        public Weapon startingWeapon = null;
        public Material destroyableMat;
        Weapon currentWeapon;
        Material lineMat;
        Health target;
        float timeSinceStartedShooting = 0.0f;
        
        private void Start() {
            currentWeapon = startingWeapon;
            timeSinceStartedShooting = Mathf.Infinity;
        }
        
        public void ShootOnce(){
            if (currentWeapon == null) return;
            currentWeapon.ShootBullet(1);
            currentWeapon.ShootBulletPrefab();
            if (currentWeapon.data.projectilePrefab != null) return;
            target = currentWeapon.Shoot();
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