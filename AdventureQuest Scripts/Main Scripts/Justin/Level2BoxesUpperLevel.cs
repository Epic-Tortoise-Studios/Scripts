using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2BoxesUpperLevel : MonoBehaviour
{

    public GameObject Smoke;
    public GameObject Landing;
    public AudioSource LandingSound;
    public Vector3 SmokeLocation;
    public GameObject Light;
    public GameObject NewLight;
    public AudioSource LSound;

    public Animator anim;
    public GameObject animSource;
    public AudioSource Explosion;
    public GameObject ExplosionSource;

    //public GameObject playerCamera;
    //public GameObject unlockCamera;

    public GameObject toSetInactive;
    public float cutTimer;

    // Start is called before the first frame update
    void Start()
    {
        Smoke = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/Smoke");
        SmokeLocation = transform.position;
        Landing = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Landing");
        LandingSound = Landing.GetComponent<AudioSource>();
        Light = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Light");
        LSound = Light.GetComponent<AudioSource>();
        //animSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Light");
        anim = GetComponent<Animator>();
        NewLight.SetActive(false);

        //playerCamera = GameObject.FindGameObjectWithTag("MainCamera");

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boxes")
        {
            LandingSound.Play();
            Instantiate(Smoke, SmokeLocation, Quaternion.identity);
            //Debug.Log("Box Active");
            NewLight.SetActive(true);
            LSound.PlayDelayed(1);
            //StartCoroutine(UnlockByLight());
            anim.SetTrigger("Pushed");
        }
    }

    /*public IEnumerator UnlockByLight()
    {
        //playerCamera.SetActive(false);
        //PlayerController.Instance.canMove = false;
        //unlockCamera.SetActive(true);
        yield return new WaitForSeconds(.5f);
        //toSetInactive.SetActive(false);
        yield return new WaitForSeconds(cutTimer);
        //playerCamera.SetActive(true);
        //PlayerController.Instance.canMove = true;
        //unlockCamera.SetActive(false);
    }*/
}
