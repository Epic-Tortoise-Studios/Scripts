using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeJumpPad : MonoBehaviour
{
    [Header("Base Variables")]
    public float padJumpForce = 45;
    public float restTimer;

    private float playerJumpForce;

    [Header("PFI Variables")]
    public AudioClip jumpClip;

    private bool isActive;
    private bool playerCheck;

    void Start()
    {
        playerJumpForce = PlayerController.Instance.jumpForce;
        isActive = true;
    }

    private void Update()
    {
        if (playerCheck)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                AudioManager.instance.PlayClip(jumpClip);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PlayerController.Instance.jumpForce = padJumpForce;
            playerCheck = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            PlayerController.Instance.jumpForce = playerJumpForce;
            playerCheck = false;

            if (isActive)
            {
                isActive = false;
                StartCoroutine(Reset());
            }
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(restTimer);
        isActive = true;
    }
}
