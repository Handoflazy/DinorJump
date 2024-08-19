using System;
using UnityEngine;
using Cinemachine;
namespace SVS.Camera
{
    public class CameraManager: MonoBehaviour
    {
        public CinemachineVirtualCamera VirtualCamera;
        private void Awake()
        {
            if(VirtualCamera == null)
            {
                VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            }
        }
        public void SetCameraTarget(Transform transform)
        {
            if(VirtualCamera == null)
            {
                VirtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            }
            VirtualCamera.LookAt = transform;
            VirtualCamera.Follow = transform;
        }

        
    }
}