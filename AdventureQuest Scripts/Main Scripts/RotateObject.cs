using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    float speed = 150.0f;
    float speed1 = -150.0f;

    public GameObject curHitObject;
    public GameObject hitTarget;

    public ParticleSystem fog;

    public AudioClip lightBeam;

    public float distance;

    public bool lit;
    public bool stopRotation;
    public bool stopCheck;

    public LayerMask layerMask;

    void Start()
    {

    }


    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, distance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.red);

            curHitObject = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * distance, Color.blue);
        }

        if(curHitObject == hitTarget && !stopCheck)
        {
            lit = true;
            stopCheck = true;

            hitTarget.GetComponentInChildren<ParticleSystem>().Play();

            fog.Stop();

            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(lightBeam);

            if (hitTarget.name.Contains("GhostWall"))
            {
                Debug.Log("Ghost Wall Check");
            }
        }


    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!stopRotation)
            {
                if (Input.GetKey(KeyCode.Q))
                {
                    transform.Rotate(Vector3.up * speed * Time.deltaTime);
                }

                if (Input.GetKey(KeyCode.E))
                {
                    transform.Rotate(Vector3.up * speed1 * Time.deltaTime);
                }
            }
        }
    }
}

