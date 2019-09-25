using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightBehavior : MonoBehaviour
{
    private GameObject ghostWall;
    private bool shiningOn;

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("GhostWall"))
        {
            Debug.Log("Spotlight Hitting Wall");
            collision.gameObject.SetActive(false);

        }
    }
}
