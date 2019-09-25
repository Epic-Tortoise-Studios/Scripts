using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpewDestroy : MonoBehaviour
{
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag != "Player" && collision.tag != "BoxDropper")
        {
            Destroy(collision.gameObject);
        }
    }

}
