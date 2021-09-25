using TopDownShooter.Combat;
using UnityEngine;
using System.Collections;
using TopDownShooter.InputHandler;

namespace TopDownShooter.Control{
    [RequireComponent(typeof(Shooter))]
    public class PlayerShooting : MonoBehaviour {
        Shooter shooter;
        InputManager input;
        private void Start() {
            shooter = GetComponent<Shooter>();
            input = GetComponent<InputManager>();
            //shooter.HandleShooting(input.FireDown);
            
        }

        
    }
}