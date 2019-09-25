using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceOpener : MonoBehaviour
{
    float speed = 150.0f;
    float speed1 = -150.0f;

    public GameObject curHitObject;
    public GameObject hitTarget;
    public GameObject SetActive;
    public GameObject SetInActive;
    public GameObject Lights;
    public GameObject LightsOn;
    public GameObject playerCamera;
    public GameObject unlockCamera;
    public AudioClip lightBeam;

    public float cutTimer;
    public float raycastDistance;

    public bool lit;
    public bool stopRotation;
    public bool stopCheck;

    public Animator Fence;
    public LayerMask layerMask;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        SetActive = GameObject.Find("***PUZZLES***/UnlockableGhostWall2/Fog");
        SetActive.SetActive(true);
        SetInActive = GameObject.Find("***PUZZLES***/UnlockableGhostWall2/Walls");
        SetInActive.SetActive(true);
        Lights = GameObject.Find("***PUZZLES***/UnlockableGhostWall2/Lights");
        Lights.SetActive(true);
        LightsOn = GameObject.Find("***PUZZLES***/UnlockableGhostWall2/Lights");
        LightsOn.SetActive(false);

        Fence = GetComponentInChildren<Animator>();
    }


    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            curHitObject = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * raycastDistance, Color.blue);
        }

        if (curHitObject == hitTarget && !stopCheck)
        {
            lit = true;
            stopCheck = true;
            stopRotation = true;

            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(lightBeam);

            StartCoroutine(UnlockByLight());
        }
        else
        {
            lit = false;
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

    public IEnumerator UnlockByLight()
    {
        playerCamera.SetActive(false);
        PlayerController.Instance.canMove = false;
        unlockCamera.SetActive(true);
        yield return new WaitForSeconds(.5f);
        SetActive.SetActive(false);
        //SetInActive.SetActive(false);
        yield return new WaitForSeconds(cutTimer);
        playerCamera.SetActive(true);
        PlayerController.Instance.canMove = true;
        unlockCamera.SetActive(false);
        Fence.SetBool("isPressed", true);
    }
}

