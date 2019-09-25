using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitTrap : MonoBehaviour
{
    public GameObject portraitWall;
    public AudioClip humanCue;
    public AudioClip ghostCue;
    public AudioClip slamNoise;
    public AudioSource aSource;
    public ParticleSystem collisionHit;
    private bool playerCheck;
    private bool triggerAttack;
    private bool stopCheck;
    private bool slamCheck;

    //Thwamp
    public Vector3 direction;
    public float units;
    public float time;
    private float speed;
    private float xMin;
    private float xMax;
    private float xMinFlipped;
    private float xMaxFlipped;
    private float zMax;
    private float zMin;
    private float zMaxFlipped;
    private float zMinFlipped;
    private float yMin;
    private float yMax;
    private float yMinFlipped;
    private float yMaxFlipped;


    public bool xAxis;
    public bool xFlipped;
    public bool zAxis;
    public bool zFlipped;
    public bool yAxis;
    public bool yFlipped;

    //Setting ForceHuman Trigger Active
    //Updated 8/21/19 by:Chris
    public GameObject forceHuman;


    void Start()
    {
        
        xMax = portraitWall.transform.position.x + units;
        xMaxFlipped = -portraitWall.transform.position.x + units;
        xMin = portraitWall.transform.position.x;
        xMinFlipped = -portraitWall.transform.position.x;

        zMax = portraitWall.transform.position.z + units;
        zMaxFlipped = -portraitWall.transform.position.z + units;
        zMin = portraitWall.transform.position.z;
        zMinFlipped = -portraitWall.transform.position.z;
    }

    void Update()
    {
        Trap();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(TransformationController.Instance.type == TransformationController.TransformationType.HUMAN && !stopCheck)
            {
                stopCheck = true;
                AudioManager.instance.audioSource = aSource;
                AudioManager.instance.PlayClip(humanCue);
            }
            else if(TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
            {
                playerCheck = true;
                triggerAttack = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (TransformationController.Instance.type == TransformationController.TransformationType.HUMAN && !stopCheck)
            {
                stopCheck = true;
                AudioManager.instance.audioSource = aSource;
                AudioManager.instance.PlayClip(humanCue);
            }
            else if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
            {
                playerCheck = true;
                triggerAttack = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            stopCheck = false;
            playerCheck = false;
            aSource.Stop();
        }
    }

    public void Trap()
    {
        if (triggerAttack)
        {
            if (xAxis)
            {
                if (direction == Vector3.right)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == Vector3.left)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (portraitWall.transform.position.x >= xMax && slamCheck)
                {
                    direction = Vector3.left;
                    StartCoroutine(TrapCosmetics());
                }

                if (portraitWall.transform.position.x <= xMin && !slamCheck)
                {
                    direction = Vector3.right;
                    triggerAttack = false;
                }
            }
            else if (xFlipped)
            {
                if (direction == Vector3.right)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == Vector3.left)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (-portraitWall.transform.position.x >= xMaxFlipped && slamCheck)
                {
                    direction = Vector3.left;
                    StartCoroutine(TrapCosmetics());
                }

                if (-portraitWall.transform.position.x <= xMinFlipped && !slamCheck)
                {
                    direction = Vector3.right;
                    triggerAttack = false;
                }
            }
            else if (zAxis)
            {
                if (direction == Vector3.forward)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == -Vector3.forward)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (portraitWall.transform.position.z >= zMax && slamCheck)
                {
                    direction = -Vector3.forward;
                    StartCoroutine(TrapCosmetics());
                }

                if (portraitWall.transform.position.z <= zMin && !slamCheck)
                {
                    direction = Vector3.forward;
                    triggerAttack = false;
                }
            }
            else if (zFlipped)
            {
                if (direction == Vector3.forward)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == -Vector3.forward)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (-portraitWall.transform.position.z >= zMaxFlipped && slamCheck)
                {
                    direction = -Vector3.forward;
                    StartCoroutine(TrapCosmetics());
                }

                if (-portraitWall.transform.position.z <= zMinFlipped && !slamCheck)
                {
                    direction = Vector3.forward;
                    triggerAttack = false;
                }
            }


            if (yAxis)
            {
                if (direction == Vector3.down)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == Vector3.up)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (portraitWall.transform.position.y >= yMax && slamCheck)
                {
                    direction = Vector3.up;
                    StartCoroutine(TrapCosmetics());
                }

                if (portraitWall.transform.position.y <= yMin && !slamCheck)
                {
                    direction = Vector3.down;
                    triggerAttack = false;
                }
            }
            else if (yFlipped)
            {
                if (direction == Vector3.down)
                {
                    forceHuman.SetActive(true);
                    speed = 40.0f;
                    slamCheck = true;
                }
                else if (direction == Vector3.up)
                {
                    forceHuman.SetActive(false);
                    speed = 10.0f;
                    slamCheck = false;
                }

                portraitWall.transform.Translate(direction * speed * Time.deltaTime);

                if (-portraitWall.transform.position.y >= yMaxFlipped && slamCheck)
                {
                    direction = -Vector3.forward;
                    StartCoroutine(TrapCosmetics());
                }

                if (-portraitWall.transform.position.y <= yMinFlipped && !slamCheck)
                {
                    direction = Vector3.forward;
                    triggerAttack = false;
                }
            }
        }
    }

    IEnumerator TrapCosmetics()
    {
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(slamNoise);
        collisionHit.Play();
        yield return new WaitForSeconds(time);
        forceHuman.SetActive(false);
        AudioManager.instance.PlayClip(ghostCue);
    }
}
