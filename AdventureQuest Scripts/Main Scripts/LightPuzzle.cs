using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightPuzzle : MonoBehaviour
{

    public GameObject lightReflector1;
    public GameObject lightReflector2;
    public GameObject lightReflector3;
    public GameObject lightReflector4;

    public bool litCheck1;
    public bool litCheck2;
    public bool litCheck3;
    public bool litCheck4;
    public bool onceCheck;
    private bool damageCheck1;
    private bool damageCheck2;
    private bool damageCheck3;
    private bool damageCheck4;
    public bool instantiate;
    public bool unlock;

    public GameObject unlockObject;
    public GameObject playerCamera;
    public GameObject unlockCamera;
    public GameObject toInstantiate;
    public GameObject instantiateTransform;
    public AudioClip enraged;

    void Start()
    {
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
    }

    
    void Update()
    {
        if (lightReflector1.GetComponent<RotateObject>().lit)
        {
            litCheck1 = true;
            lightReflector1.GetComponent<RotateObject>().stopRotation = true;

            if (!damageCheck1)
            {
                GameManager.Instance.bossUI.GetComponentInChildren<Slider>().value -= 25;
                damageCheck1 = true;
            }
        }

        if (lightReflector2.GetComponent<RotateObject>().lit)
        {
            litCheck2 = true;
            lightReflector2.GetComponent<RotateObject>().stopRotation = true;

            if (!damageCheck2)
            {
                GameManager.Instance.bossUI.GetComponentInChildren<Slider>().value -= 25;
                damageCheck2 = true;
            }
        }

        if (lightReflector3.GetComponent<RotateObject>().lit)
        {
            litCheck3 = true;
            lightReflector3.GetComponent<RotateObject>().stopRotation = true;

            if (!damageCheck3)
            {
                GameManager.Instance.bossUI.GetComponentInChildren<Slider>().value -= 25;
                damageCheck3 = true;
            }
        }

        if (lightReflector4.GetComponent<RotateObject>().lit)
        {
            litCheck4 = true;
            lightReflector4.GetComponent<RotateObject>().stopRotation = true;

            if (!damageCheck4)
            {
                GameManager.Instance.bossUI.GetComponentInChildren<Slider>().value -= 25;
                damageCheck4 = true;
            }
        }

        if (litCheck1 && litCheck2 && litCheck3 && litCheck4 && !onceCheck)
        {
            this.gameObject.GetComponent<Shockwave>().stopSlam = true;
            GameManager.Instance.bossBattle = false;
            StartCoroutine(Unlocked());
            onceCheck = true;
        }
    }

    IEnumerator Unlocked()
    {
        if(unlock && unlockCamera != null)
        {
            playerCamera.SetActive(false);
            PlayerController.Instance.canMove = false;
            unlockCamera.SetActive(true);
            unlockObject.GetComponent<Animator>().SetBool("Unlocked", true);
            yield return new WaitForSeconds(2);
            playerCamera.SetActive(true);
            PlayerController.Instance.canMove = true;
            unlockCamera.SetActive(false);
        }
        else if(instantiate && unlockCamera != null)
        {
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(enraged);
            yield return new WaitForSeconds(3);
            playerCamera.SetActive(false);
            PlayerController.Instance.canMove = false;
            unlockCamera.SetActive(true);
            Instantiate(toInstantiate, instantiateTransform.transform);
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(GameManager.Instance.positiveAudio);
            yield return new WaitForSeconds(3);
            playerCamera.SetActive(true);
            PlayerController.Instance.canMove = true;
            unlockCamera.SetActive(false);
        }
        yield return null;
    }
}
