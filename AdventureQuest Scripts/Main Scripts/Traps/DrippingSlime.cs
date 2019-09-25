using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrippingSlime : MonoBehaviour
{
    public float dripTimer;
    public GameObject slimeDrop;

    private float timer;
    private bool canDrip;

    void Start()
    {
        timer = dripTimer;
        canDrip = true;
    }

    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if(timer <= 0 && canDrip)
        {
            canDrip = false;
            Instantiate(slimeDrop, transform.position + transform.up * -1, transform.rotation);
            Drip();
        }
    }

    void Drip()
    {
        int rand = Random.Range(0, 4);

        if (rand == 0)
        {
            timer = 2;
            canDrip = true;
        }
        if (rand == 1)
        {
            timer = 3;
            canDrip = true;
        }
        if (rand == 2)
        {
            timer = 4;
            canDrip = true;
        }
        if (rand == 3)
        {
            timer = 5;
            canDrip = true;
        }
    }
}
