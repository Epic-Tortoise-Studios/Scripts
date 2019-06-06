using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyJump : MonoBehaviour
{

    public float force;
    float jumpTimer;
    public float maxJumpTimer;
    
    void Update()
    {
        jumpTimer -= Time.deltaTime;

        if (jumpTimer <= 0)
        {
            jumpTimer = maxJumpTimer;
            Jump();
        }
    }

    void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * force);
    }
   
}
