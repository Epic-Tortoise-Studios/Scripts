
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chandelier : MonoBehaviour
{
    [Header("Base Variables")]
    public float fallDistance;
    public float fallTimer;
    public float resetTimer;

    private Vector3 posA;
    private Vector3 posB;


    [Header("Object Float Variables")]
    public float maxAmplitude;
    public float minAmplitude;
    private float amplitude;
    public float maxFrequency;
    public float minFrequency;
    private float frequency;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    [Header("PFI Variables")]
    public AudioClip hitClip;
    public GameObject snapClipSource;
    public AudioSource snapClip;
    public AudioClip crashClip;

    private bool playerCheck;
    private bool coroutineCheck;
    private bool falling;
    public Animator anim;
    
    void Start()
    {
        anim = GetComponent<Animator>();
        snapClipSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Snap");
        snapClip = snapClipSource.GetComponent<AudioSource>();
        falling = false;
        posOffset = transform.position;
        posA = gameObject.transform.position;
        posB = new Vector3(transform.position.x, transform.position.y - fallDistance, transform.position.z);

        frequency = Random.Range(maxFrequency, minFrequency);
        amplitude = Random.Range(maxAmplitude, minAmplitude);
    }

    private void Update()
    {
        Float();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            anim.SetTrigger("SteppedOn");
            playerCheck = true;

            if(!falling)
            {
                falling = true;
                StartCoroutine(Movement());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerCheck = false;
        }
    }


    private void Float()
    {
        if (!playerCheck && !coroutineCheck)
        {
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
            transform.position = tempPos;
        }
    }

    IEnumerator Movement()
    {
        coroutineCheck = true;

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(hitClip);
        snapClip.PlayDelayed(fallTimer - 0.2f);

        yield return new WaitForSeconds(fallTimer);
        float startTime = Time.time;
        while (Time.time - startTime <= 1)
        { 
            transform.position = Vector3.Lerp(posA, posB, Time.time - startTime);
            yield return 1; 
        }

        AudioManager.instance.PlayClip(crashClip);

        yield return new WaitForSeconds(resetTimer);
        transform.position = Vector3.Lerp(posB, posA, Time.time - startTime);
        falling = false;
        coroutineCheck = false;
    }
}

