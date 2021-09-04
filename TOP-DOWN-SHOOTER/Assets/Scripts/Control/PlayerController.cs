using System;
using System.Collections;
using System.Collections.Generic;
using TopDownShooter.Data;
using TopDownShooter.InputHandler;
using UnityEngine;

namespace TopDownShooter.Control
{
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerController : MonoBehaviour
    {
        public D_Player data;
        private Rigidbody rb;
        private InputManager input;

        Vector3 mouseDir;
        Vector3 movementVector;
        PlayerConstrains constrains = new PlayerConstrains();
        CameraController cameraController;
        private void Start() {
            rb = GetComponent<Rigidbody>();
            input = GetComponent<InputManager>();
            cameraController = FindObjectOfType<CameraController>();
        }
        private void Update() {
            CheckIfCanMove();
            LookAtCursor();
            
        }
        private void FixedUpdate() {
            Move();
        }

        private void LookAtCursor(){
            if (constrains.canMove)
            {
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
                movementVector = transform.right * input.AxisInput.x +
                transform.forward * input.AxisInput.y;
                movementVector.y = rb.velocity.y;
            }else{
                movementVector = Vector3.zero;
            }
            
        }

        private void CheckIfCanMove(){
            float distance = Vector3.Distance(transform.position , input.MousePosition);
            //print(distance);
            constrains.canMove = distance > data.minMouseDistance;
        }

       
        [System.Serializable]
        public struct PlayerConstrains{
            public bool canMove;
        }
        
        
    }

}