using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public Rigidbody targetRB;
    public float gravityScale;
    public static float globalGravity = -9.81f;

    void Start()
    {
        targetRB = GetComponent<Rigidbody>();
        targetRB.useGravity = false;

    }


    void Update()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        targetRB.AddForce(gravity, ForceMode.Acceleration);
    }
}
