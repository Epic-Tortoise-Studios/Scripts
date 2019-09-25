using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{
    public Vector3 yPosition;
    public Transform Position;
    public GameObject Cube;
    public AudioSource Falling;
    public Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        Falling = GetComponent<AudioSource>();
        yPosition = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
