
using UnityEngine;
using System.Collections;
using TopDownShooter.UI;


namespace  TopDownShooter.Combat
{
    public class Weapon : MonoBehaviour{
        public TextDisplay bulletMagDisplay = null;
        public Transform shootPoint;
        public WeapoData data;
        public ParticleSystem muzzleFlash = null;

        public Vector3 aimHitPint{get;private set;}
        public int magSize{get;private set;}
        public int currentBulletCount{get;private set;}
        float timeSinceDrawnBullet = 0.0f;
        public WeaponConstrains constrains;
        private void Start() {
            magSize = data.startingMagSize;
            currentBulletCount = magSize;
            bulletMagDisplay = GameObject.FindGameObjectWithTag("BulletDisplay").GetComponent<TextDisplay>();
            if (bulletMagDisplay != null){
                bulletMagDisplay.SetText(currentBulletCount.ToString());
            }
            constrains = new WeaponConstrains();
        }
        public Health Shoot(Vector3 mousePos){
            if (shootPoint == null){
                print("Shootpoint not assigned");
                return null;
            } 
            RaycastHit hitInfo;
            Vector3 dir = GetRecoilDir(Random.Range(data.recoilAmmount, -data.recoilAmmount) , mousePos);
            bool hit = Physics.Raycast(shootPoint.position , dir , out hitInfo ,Mathf.Infinity);
            if (!hit){
                aimHitPint = Vector3.zero;
                return null;
            }
            aimHitPint = hitInfo.point;
            if (data.bullHitEffect != null){
                Instantiate(data.bullHitEffect, aimHitPint, Quaternion.identity);
            }
            
            return hitInfo.collider.GetComponent<Health>();
        }
        public void ShootBulletPrefab(Vector3 mousePos ){
            if (data.projectilePrefab == null) return; 
            Projectile bullet = Instantiate(data.projectilePrefab , shootPoint.position , shootPoint.rotation) as Projectile;
            bullet.InitializeBullet(GetRecoilDir( Random.Range( data.recoilAmmount,-data.recoilAmmount) , mousePos)* data.projectileSpeed , data.startingDamage);
        }
        
        public void DrawBulletLine(){
            if (data.bulletLine == null) return;
            BulletDraw bullet = Instantiate(data.bulletLine , transform.position , Quaternion.identity) as BulletDraw;
            bullet.SetPositions(shootPoint.position , aimHitPint);
        }
        
        public void Reload(int bullets){
           if (!constrains.isReloading){
               StartCoroutine(ReloadMag(bullets));
           }

        }
        IEnumerator ReloadMag(int bullets){
            constrains.isReloading = true;
            yield return new WaitForSeconds(data.reloadTime);
            currentBulletCount = Mathf.Min(magSize , currentBulletCount + bullets);
            bulletMagDisplay.SetText(currentBulletCount.ToString());
            constrains.isReloading = false;

        }

        public void ShootBullet(int bullets){

            currentBulletCount = Mathf.Max(0 , currentBulletCount - 1);
            if (bulletMagDisplay != null){
                bulletMagDisplay.SetText(currentBulletCount.ToString());
            }else{
                print("Bullet Mag Display Reference is null");
            }
            if (muzzleFlash == null) return;
            muzzleFlash.Play();
            

        }
        public Vector3 GetRecoilDir(float angle , Vector3 mousePos){
            angle += transform.eulerAngles.y;
            Vector3 dir = (mousePos - shootPoint.position).normalized;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) , dir.y , Mathf.Cos(angle * Mathf.Deg2Rad));
        }

        public struct WeaponConstrains{
            public bool isReloading;
        }
       
        private void OnDrawGizmos() {
            if (shootPoint == null) return;
            Gizmos.DrawLine(shootPoint.position , shootPoint.position + shootPoint.forward * 50f);
        }
        

    }

}