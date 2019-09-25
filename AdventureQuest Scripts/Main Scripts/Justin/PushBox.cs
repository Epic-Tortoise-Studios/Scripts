using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBox : MonoBehaviour
{

    public GameObject Player;
    public AudioSource Dragging;
    public GameObject DraggingSource;
    public GameObject Cube;
    public MeshRenderer CubeColor;

    float duration = 1.5f;
    public float t;
    bool isReset = false;

    public AudioSource PushingClick;
    public GameObject Pushing;

    public Animator anim;
    public string animationparameter = "please put in parameters";



    // Start is called before the first frame update
    void Start()
    {
        DraggingSource = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Dragging");
        Dragging = DraggingSource.GetComponent<AudioSource>();
        Player = GameObject.FindGameObjectWithTag("Player");
        GetComponent<Renderer>().material.color = Color.green;
        Pushing = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxAudio/Pushing");
        PushingClick = Pushing.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Dragging.Play();
            PlayerController.Instance.canDash = false;
            ColorChanger();
            PushingClick.Play();
            anim.SetBool(animationparameter, true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Dragging.Pause();
            PlayerController.Instance.canDash = true;
            ColorChangeBacker();
            PushingClick.Play();
            anim.SetBool(animationparameter, false);
        }
    }

    /*void PlayDrag()
    {
        Dragging.Play();
    }

    void PauseDrag()
    {
        Dragging.Pause();
    }*/

    void ColorChanger()
    {
            GetComponent<Renderer>().material.color = Color.Lerp(Color.green, Color.red, Mathf.PingPong(Time.time, 1));
                //t += Time.deltaTime / duration;
    }

    void ColorChangeBacker()
    {
            GetComponent<Renderer>().material.color = Color.Lerp(Color.red, Color.green, Mathf.PingPong(Time.time, 1));
                //t += Time.deltaTime / duration;
    }

    /*void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dragging.Play();

            //Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
            //PlayerController.Instance.anim.SetBool("isDashing", false);
            Debug.Log("Player Entered");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //Physics.IgnoreCollision(Player.GetComponent<Collider>(), GetComponent<Collider>());
            //PlayerController.Instance.anim.SetBool("isDashing", false);
            Debug.Log("Player Staying");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Dragging.Pause();
            //collision.TransformationController.Instance.type = collision.TransformationController.TransformationType.BEAST;
            Debug.Log("Player Leaving");
        }
    }*/
}
