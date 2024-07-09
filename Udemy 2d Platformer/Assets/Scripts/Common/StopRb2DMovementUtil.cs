using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopRb2DMovementUtil : MonoBehaviour
{
    Rigidbody2D rb2d;
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
   public void StopMovement()
    {
        rb2d.velocity = Vector3.zero;
    }
}
