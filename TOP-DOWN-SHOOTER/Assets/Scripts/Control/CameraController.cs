using UnityEngine;
using Cinemachine;
using System;

namespace TopDownShooter.Control{


    public class CameraController : MonoBehaviour {
        CinemachineVirtualCamera virtualCamera;
        CinemachineBasicMultiChannelPerlin perlinNoise;
        
        float shakeTime;
        private void Start() {
            virtualCamera = GetComponent<CinemachineVirtualCamera>();
            perlinNoise = virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            
            perlinNoise.m_AmplitudeGain = 0.0f;
        }

        private void Update() {
            perlinNoise.m_AmplitudeGain =
            Mathf.Max(0.0f, perlinNoise.m_AmplitudeGain - Time.deltaTime / shakeTime);
        }

        public void Shake(float ammount , float time){
            perlinNoise.m_AmplitudeGain = ammount;
            shakeTime = time;
        }

        

        
    }
}