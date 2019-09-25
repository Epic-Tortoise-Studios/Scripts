using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    public GameObject playerCamera;
    public GameObject unlockCamera;
    public GameObject unlockCamera2;
    public GameObject requirementObject;
    public GameObject requirementObject2;
    public GameObject requirementObject3;

    public AudioClip audioCue;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(StartIntroCutscene());
            TransformationController.Instance.canTransform = true;
        }
    }

    IEnumerator StartIntroCutscene()
    {
        playerCamera.SetActive(false);
        PlayerController.Instance.canMove = false;
        unlockCamera.SetActive(true);
        requirementObject.GetComponent<Animator>().SetBool("Cutscene", true);
        yield return new WaitForSeconds(.02f);
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(audioCue);
        requirementObject2.GetComponent<Animator>().SetBool("Fade", true);
        yield return new WaitForSeconds(2);
        unlockCamera.SetActive(false);
        unlockCamera2.SetActive(true);
        requirementObject3.SetActive(true);
        yield return new WaitForSeconds(5);
        playerCamera.SetActive(true);
        PlayerController.Instance.canMove = true;
        unlockCamera2.SetActive(false);
        this.gameObject.SetActive(false);
        requirementObject3.SetActive(false);
    }
}
