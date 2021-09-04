using UnityEngine;

namespace TopDownShooter.InputHandler
{
    
    public class InputManager : MonoBehaviour {
        [SerializeField] LayerMask groundMask;
        public Vector2 AxisInput{ get ; private set ;}
        public Vector3 MousePosition{ get ; private set ;}
        public bool FireDown{ get ; private set; }
        public bool FireDownOnce { get; private set; }

        Camera mainCam;
        RaycastHit[] hitInfos;
        private void Start() {
            mainCam = Camera.main;
        }

        private void Update() {
            AxisInput = new Vector2(Input.GetAxisRaw("Horizontal"),Input.GetAxisRaw("Vertical")).normalized;
            GetMousePos();
            FireDownOnce = Input.GetButtonDown("Fire1");
            FireDown = Input.GetButton("Fire1");
        }
        
        void GetMousePos(){
            if (mainCam.orthographic){
                Vector3 pos = mainCam.ScreenToWorldPoint(Input.mousePosition);
                MousePosition = new Vector3(pos.x , transform.position.y , pos.z);
            }else{
                Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
                hitInfos = Physics.RaycastAll(ray.origin, ray.direction, Mathf.Infinity, groundMask);
                if (hitInfos.Length > 0){
                    MousePosition = hitInfos[0].point;
                }
                
            }
            
            
        }

    }
}