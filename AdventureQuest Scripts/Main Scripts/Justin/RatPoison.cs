using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatPoison : MonoBehaviour
{

    public GameObject ratPoison;
    public float timer;
    public Vector3 MouseVector;

    // Start is called before the first frame update
    void Start()
    {
        //ratPoison = GameObject.Find("MousePoison");
        timer = 0.3f;
        InvokeRepeating("RatPoisonEmission", 0, timer);
    }

    // Update is called once per frame
    void Update()
    {
        MouseVector = gameObject.transform.position;
    }

    void RatPoisonEmission()
    {
        Instantiate(ratPoison, MouseVector, Quaternion.identity);
    }
}
