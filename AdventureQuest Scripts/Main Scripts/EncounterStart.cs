using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterStart : MonoBehaviour
{

    public GameObject panCamera;
    public GameObject playerCamera;
    public GameObject puzzle;
    public AudioClip spiritGiggle;
    public GameObject[] spirits;
    public GameObject[] fog;


    private bool stopCheck;

    private void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

        foreach (GameObject f in fog)
        {
            if (f != null)
            {
                f.GetComponent<ParticleSystem>().Stop();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (stopCheck == false)
            {
                GameManager.Instance.bossBattle = true;
                StartCoroutine(BeginEncounter());
                stopCheck = true;
            } 
        }
    }

    IEnumerator BeginEncounter()
    {
        playerCamera.SetActive(false);
        PlayerController.Instance.canMove = false;
        panCamera.SetActive(true);
        panCamera.GetComponent<Animator>().SetBool("Pan", true);
        puzzle.GetComponent<Shockwave>().stopSlam = false;

        yield return new WaitForSeconds(.5f);

        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                //s.GetComponent<Animator>().SetBool("Fade", true);
            }
        }
        yield return new WaitForSeconds(.5f);

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(spiritGiggle);

        yield return new WaitForSeconds(1f);

        foreach (GameObject f in fog)
        {
            if (f != null)
            {
                f.GetComponent<ParticleSystem>().Play();
            }
        }

        yield return new WaitForSeconds(2.65f);

        playerCamera.SetActive(true);
        PlayerController.Instance.canMove = true;
        panCamera.SetActive(false);

        foreach (GameObject s in spirits)
        {
            if (s != null)
            {
                s.SetActive(false);
            }
        }

        yield return null;
    }
}
