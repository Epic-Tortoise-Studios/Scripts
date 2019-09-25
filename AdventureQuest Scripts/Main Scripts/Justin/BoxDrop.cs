using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxDrop : MonoBehaviour
{
    public Transform BoxHoleTransform;
    public Transform BoxTransform;
    public Transform newBoxTransform;
    public Vector3 BoxHoleVector3;
    public Vector3 BoxVector3;
    public Vector3 newBoxVector3;
    public float Vectorx, Vectory, Vectorz;
    public Rigidbody BoxRigidbody;
    public GameObject BoxHole;
    public GameObject Box;
    private float xPosition, yPosition, zPosition;
    public float startTime;
    public float journeyLength;
    public float speed = 1.0F;
    public bool Moving = false;
    public float pause;
    public bool canDash;
    public GameObject Player;
    public Transform PlayerTransform;
    public AudioSource Dragging;
    public GameObject DraggingPlace;
    public GameObject Landing;
    public AudioSource LandingSound;
    public GameObject Smoke;

    public Vector3 SmokeLocation;
    public float mag;
    public float minMoveSpeed = .2f;
    public float rotatespeed;


    public Animator m_Animator;

    public RigidbodyConstraints originalConstraints;

    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponentInChildren<Animator>();
        DraggingPlace = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Dragging");
        Landing = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Landing");
        LandingSound = Landing.GetComponent<AudioSource>();
        Dragging = DraggingPlace.GetComponent<AudioSource>();
        canDash = true;
        BoxRigidbody = GetComponent<Rigidbody>();
        BoxVector3 = BoxRigidbody.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerTransform = Player.transform;
        Smoke = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/Smoke");
        SmokeLocation = transform.position;
        SmokeLocation = new Vector3(xPosition, yPosition, zPosition);
    }

    // Update is called once per frame
    void Update()
    {
        mag = BoxRigidbody.velocity.magnitude;
        BoxVector3 = transform.position;
        //DraggingNoise();
        Freeze();
    }

    void OnTriggerEnter(Collider BoxHoleTrigger)
    {
        float step = speed * Time.deltaTime;
        if(BoxHoleTrigger.tag == "BoxDropper")
        {
            BoxRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX  | RigidbodyConstraints.FreezeRotation;
            //Falling();
        }

        if(BoxHoleTrigger.tag == "Mana")
        {
            //LandingSound.Play();
            Instantiate(Smoke, new Vector3(transform.position.x, (transform.position.y - 5.5f), transform.position.z), 
                Quaternion.RotateTowards(transform.rotation, PlayerTransform.rotation, step));
        }
    }

    void DraggingNoise()
    {
        if (mag >= 1 && (m_Animator.GetBool("isPressed")) && !Dragging.isPlaying)
        {
            Dragging.Play();
            Debug.Log("Drag Playing");
        }

        else if (mag < 1 && Dragging.isPlaying)
        {
            Dragging.Pause();
            Debug.Log("Drag doing nothing");
        }
        
        else
        {
            Dragging.Pause();
            Debug.Log("Drag Stopped");
        }

        //Debug.Log("Magnitude " + mag);

        /*if(newBoxVector3 != transform.position && !Dragging.isPlaying)
       {
           Dragging.Play();
           Debug.Log("Drag Playing");
       }

       else if(newBoxVector3 == transform.position && Dragging.isPlaying)
       {
           Dragging.Pause();
           Debug.Log("Drag doing nothing");
       }

       else if(newBoxVector3 != transform.position && Dragging.isPlaying)
       {
       Dragging.Pause();
       Debug.Log("Drag doing nothing");
       }*/

        //if (mag > minMoveSpeed || mag < minMoveSpeed)
        /*
        if (mag == 0 && (m_Animator.GetBool("isPressed")))
        {
            Dragging.Play();
            Debug.Log("Drag Playing");
        } 

        else if (mag == 0  && !(m_Animator.GetBool("isPressed")))
        {
            Dragging.Pause();
            Debug.Log("Drag Stopping");
        }
        else if (mag == 0 && (m_Animator.GetBool("isPressed")))
        {
            Dragging.Play();
            Debug.Log("Drag Playing");
        }

        else if (mag != 0 && (m_Animator.GetBool("isPressed")))
        {
            Dragging.Pause();
            Debug.Log("Drag Stopping");
        }

        else if (mag != 0)
        {
            Dragging.Pause();
            Debug.Log("Drag Stopping");
        }*/

        //Debug.Log("Magnitude " + BoxRigidbody.velocity.magnitude);
    }

    void Freeze()
    {
        if (m_Animator.GetBool("isPressed"))
        {
            BoxRigidbody.constraints = RigidbodyConstraints.None;
            BoxRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        }

        else
        {
            BoxRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        }
    }
    

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(pause);
    }

    void Falling()
    {
        BoxRigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotation;
        //Debug.Log("Can Fall");
    }
}
