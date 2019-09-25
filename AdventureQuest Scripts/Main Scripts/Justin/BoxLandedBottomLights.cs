using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxLandedBottomLights : MonoBehaviour
{
    public GameObject Smoke;
    public GameObject Landing;
    public AudioSource LandingSound;
    public Vector3 SmokeLocation;
    public GameObject Light;
    public GameObject NewLight;
    public AudioSource LSound;

    // Start is called before the first frame update
    void Start()
    {
        Smoke = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/Smoke");
        SmokeLocation = transform.position;
        Landing = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Landing");
        LandingSound = Landing.GetComponent<AudioSource>();
        Light = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Light");
        LSound = Light.GetComponent<AudioSource>();
        NewLight.SetActive(false);
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
        }
    }
}
