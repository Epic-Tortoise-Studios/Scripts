using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxStopperReset : MonoBehaviour
{
    public GameObject Hole;
    public GameObject Cube;
    public Vector3 xHole, yHole, zHole;
    public Transform tHole;
    public Transform tCube;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boxes")
        {
            //Reset the Position of the Z and X to the position of the hole
        }
    }

}
