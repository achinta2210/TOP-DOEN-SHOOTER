
using TopDownShooter.Combat;
using TopDownShooter.Data;
using TopDownShooter.InputHandler;
using UnityEngine;

namespace TopDownShooter.Control
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Shooter))]
    public class PlayerController : MonoBehaviour
    {
        public D_Player data;
        public Animator animator;
        //public Transform bodyTransform;
        private Rigidbody rb;
        private InputManager input;
        const string xVelocity = "xVelocity";
        const string zVelocity = "zVelocity";
        Vector3 mouseDir;
        Vector3 movementVector;
        PlayerConstrains constrains = new PlayerConstrains();
        Shooter shooter;
        private void Start() {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<InputManager>();
            if (animator == null) animator = GetComponent<Animator>();
            shooter = GetComponent<Shooter>();
        }
        private void Update() {
            CheckIfCanMove();
            LookAtCursor();
            HandleAnimation();
            HandleCombat();
        }
        private void FixedUpdate() {
            Move();
        }

        private void LookAtCursor(){
            if (constrains.canMove){
                Vector3 dir = (input.MousePosition - transform.position).normalized;
                mouseDir.Set(dir.x, 0.0f, dir.z);
                transform.rotation = Quaternion.LookRotation(mouseDir);
            }
            
        }

        private void Move(){
            
            CalculateMovementVector();
            rb.velocity = movementVector * data.movementSpeed;
        }

        private void CalculateMovementVector(){
            if (constrains.canMove){
                movementVector = Vector3.right * input.AxisInput.x +
                transform.forward * input.AxisInput.y;
                movementVector.y = rb.velocity.y;
            }else{
                movementVector = Vector3.zero;
            }
            
        }

        private void CheckIfCanMove(){
            float distance = Vector3.Distance(transform.position , input.MousePosition);
            constrains.canMove = distance > data.minMouseDistance;
        }
        public void HandleAnimation(){
            
            if (Mathf.Abs(rb.velocity.magnitude) > 0.01f){
                animator.SetBool("Run" , true);
                animator.SetFloat(xVelocity, Mathf.Sign(rb.velocity.magnitude));
            }else{
                animator.SetBool("Run", false);
            }
            
        }
        public void HandleCombat(){
            shooter.AutometicShoot(input.FireDown);
        }
        
        [System.Serializable]
        public struct PlayerConstrains{
            public bool canMove;
        }
        
        
    }

}