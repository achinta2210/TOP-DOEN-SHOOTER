using UnityEngine;

namespace TopDownShooter.Core{

    [RequireComponent(typeof(MeshRenderer))]
    [RequireComponent(typeof(MeshFilter))]
    public class FieldOfView : MonoBehaviour{
        public float viewRadious = 20f;
        public float fieldOfView = 15f;
        public float viewDistance = 7f;
        public int rayCount = 10;
        public LayerMask obstacleMask;
        public int cornerDetectionIterations = 5;
        float angleBetweenRay;
        Vector3[] vertices;
        int[] tris;
       
        RayData newRayData , oldRayData;
        MeshFilter meshFilter;
        Mesh fovMesh;
        private void Start() {
            vertices = new Vector3[rayCount + 1];
            tris = new int[(rayCount + 1) * 3];
            vertices[0] = Vector3.zero;
            angleBetweenRay = (fieldOfView  * 2/ rayCount ) ;
            angleBetweenRay = angleBetweenRay + angleBetweenRay/(rayCount - 1);

            fovMesh = new Mesh();
            fovMesh.name = "Field Of View";
            GetComponent<MeshFilter>().mesh = fovMesh;
        }

        private void LateUpdate() {
            DrawFieldOfView();
            BuildMesh();
        }

        public void DrawFieldOfView(){
            int triagleIndex = 0;
            for (int i = 0; i < rayCount; i++){
                float angle = fieldOfView - angleBetweenRay * i + transform.eulerAngles.y;
                newRayData = CastRayData(angle);
                vertices[i + 1] = newRayData.hitLocalPoint;
                
                if (triagleIndex != 0)
                {
                    tris[triagleIndex] = 0;
                    tris[triagleIndex + 1] = i + 1;
                    tris[triagleIndex + 2] = i;
                }
                oldRayData = newRayData;
                triagleIndex = triagleIndex + 3;
            }
        }
        public void CheckForCorner(int index , RayData newEdgeRayData , RayData oldEdgeRayData) {

            if ( (newEdgeRayData.hasHit && !oldRayData.hasHit) || (!oldEdgeRayData.hasHit && oldRayData.hasHit)){
                for (int i = 1; i <= cornerDetectionIterations; i++){
                    newEdgeRayData = CastRayData(angleBetweenRay /2 * i );
                    if (newEdgeRayData.hasHit && !oldRayData.hasHit){
                        
                    }else if (!newEdgeRayData.hasHit && oldRayData.hasHit){

                    }
                }
            }
        }
        public void BuildMesh(){
            fovMesh.Clear();
            fovMesh.vertices = vertices;
            fovMesh.triangles = tris;
        }
        public RayData CastRayData(float angle){
            RaycastHit hitInfo;
            bool hit = Physics.Raycast(transform.position , GetDirFromAngle(angle) , out hitInfo ,viewDistance ,obstacleMask);
            if (hit)
            {
                return new RayData(true , transform.InverseTransformPoint(hitInfo.point));
            }else{
                return new RayData(false , transform.InverseTransformPoint(transform.position + GetDirFromAngle(angle) * viewDistance));
            }
            
        }

        public Vector3 GetDirFromAngle(float angle){
            return new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad) , 0 ,Mathf.Cos(angle * Mathf.Deg2Rad)).normalized;
        }
        
        public struct RayData{
            public bool hasHit;
            public Vector3 hitLocalPoint;
            public RayData(bool _hasHit , Vector3 _hitLocalPoint){
                hasHit = _hasHit;
                hitLocalPoint = _hitLocalPoint;
            }
        }



        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position , viewRadious);
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position ,transform.position + GetDirFromAngle(transform.eulerAngles.y + fieldOfView) * viewDistance);
            Gizmos.DrawLine(transform.position, transform.position + GetDirFromAngle(transform.eulerAngles.y - fieldOfView) * viewDistance);
        }
    }
}