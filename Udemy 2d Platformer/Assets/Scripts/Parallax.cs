using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Camera _camera;
    private Vector2 startPos;
    [SerializeField,Range(0,1)]
    private float offSetParralax = -0.15f;
    private Vector2 travel =>(Vector2)_camera.transform.position-startPos;
    private void Awake()
    {
        _camera = Camera.main;
    }
    private void Start()
    {
        startPos = transform.position;
    }
    private void FixedUpdate()
    {
        transform.position =   new Vector2((startPos +travel*offSetParralax).x,0);
    }
}
