using UnityEngine;

namespace TopDownShooter.Combat{

    [CreateAssetMenu(fileName = "WeapoData", menuName = "Data/new Weapon Data", order = 0)]
    public class WeapoData : ScriptableObject {
        
        public float startingDamage = 15f;
        public float range = 20f;
        [Range(0.1f , 1f)]
        public float fireRate = 0.1f;
        [Range(0.1f, 10f)]
        public float recoilAmmount = 1f;
        public float reloadTime = 2.0f;
        public float bulletDisplayTime = 0.1f;
        public int startingMagSize = 30;
        public Projectile projectilePrefab = null; 
        public BulletDraw bulletLine = null;
        public Effect bullHitEffect = null;
        public float projectileSpeed = 100f;
    }
}