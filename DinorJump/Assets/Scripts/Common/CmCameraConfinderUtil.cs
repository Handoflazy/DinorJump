using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SVS.Camera
{
    public class CmCameraConfinderUtil : MonoBehaviour
    {
        public PolygonCollider2D cameraConfiner;
        public CinemachineConfiner2D cm_confinder;

        public void SetConfiner()
        {
            cm_confinder.m_BoundingShape2D = cameraConfiner;
        }
    }
}