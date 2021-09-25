
using UnityEngine;

namespace TopDownShooter.Combat{
    [RequireComponent(typeof(LineRenderer))]
    public class BulletDraw : MonoBehaviour {
        public float startingAlpha = 0.5f;
        public Color color;
        public float moveSpeed = 10f;
        public float bulletFadeTime = 0.4f;
        LineRenderer line;
        float alpha = 1.0f;
        Transform originPoint;
        private void OnEnable() {
            line = GetComponent<LineRenderer>();
            alpha = startingAlpha;
            SetAlpha(alpha);
            Destroy(gameObject, bulletFadeTime);
        }
        
        private void Update() {
            
            alpha = Mathf.Clamp01(alpha -= (Time.deltaTime/bulletFadeTime) * startingAlpha);
            SetAlpha(alpha);
            line.SetPosition(0 , Vector3.MoveTowards(line.GetPosition(0) , line.GetPosition(1), moveSpeed * Time.deltaTime));
            if (Vector3.Distance(line.GetPosition(0),line.GetPosition(1)) < 0.1f){
                Destroy(gameObject);
            }
            
        }

        
        void SetAlpha(float alpha){
            line.startColor = new Color(color.r , color.g , color.b , alpha);
            line.endColor = new Color(color.r, color.g, color.b, alpha);

        }

        public void SetPositions(Vector3 startPos, Vector3 endPos){
            line.SetPosition(0, startPos);
            line.SetPosition(1, endPos);
        }

        

    }
}
