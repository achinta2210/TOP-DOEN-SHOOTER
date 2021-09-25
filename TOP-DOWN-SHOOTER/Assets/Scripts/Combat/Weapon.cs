
using UnityEngine;

namespace  TopDownShooter.Combat
{
    public class Weapon : MonoBehaviour{
        
        public Transform shootPoint;
        public WeapoData data;
        public ParticleSystem muzzleFlash = null;

        public Vector3 aimHitPint{get;private set;}
        public int magSize{get;private set;}
        public int currentBulletCount{get;private set;}
        float timeSinceDrawnBullet = 0.0f;
       
        public Health Shoot(){
            if (shootPoint == null){
                print("Shootpoint not assigned");
                return null;
            } 
            RaycastHit hitInfo;
            Vector3 dir = GetRecoilDir(Random.Range(data.recoilAmmount, -data.recoilAmmount));
            bool hit = Physics.Raycast(shootPoint.position , dir , out hitInfo ,data.range);
            if (!hit){
                aimHitPint = shootPoint.position + dir * data.range;
                return null;
            }
            aimHitPint = hitInfo.point; 
            return hitInfo.collider.GetComponent<Health>();
        }
        public void ShootBulletPrefab(){
            if (data.projectilePrefab == null) return; 
            Projectile bullet = Instantiate(data.projectilePrefab , shootPoint.position , shootPoint.rotation) as Projectile;
            bullet.InitializeBullet(GetRecoilDir( Random.Range( data.recoilAmmount,-data.recoilAmmount))* data.projectileSpeed , data.startingDamage);
        }
        
        public void DrawBulletLine(){
            if (data.bulletLine == null) return;
            BulletDraw bullet = Instantiate(data.bulletLine , transform.position , Quaternion.identity) as BulletDraw;
            bullet.SetPositions(shootPoint.position , aimHitPint);
        }
        
        public void Reaload(int bullets){
            currentBulletCount = Mathf.Min(magSize , currentBulletCount + bullets);
        }
        public void Reload(){
            currentBulletCount = Mathf.Min(magSize, magSize - currentBulletCount);

        }

        public void ShootBullet(int bullets){

            currentBulletCount = Mathf.Max(0 , currentBulletCount - 1);
            if (muzzleFlash == null) return;
            muzzleFlash.Play();

        }
        public Vector3 GetRecoilDir(float angle){
            angle += transform.eulerAngles.y;
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) , 0.0f , Mathf.Cos(angle * Mathf.Deg2Rad));
        }
       
        private void OnDrawGizmos() {
            if (shootPoint == null) return;
            Gizmos.DrawLine(shootPoint.position , shootPoint.position + shootPoint.forward * 50f);
        }
        

    }

}