using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region Singleton
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerController>();
            return instance;
        }
    }
    #endregion

    public float currentSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float jumpForce;
    public float gravityScale;
    public float rotateSpeed;
    public float maxDashTime = 1.0f;
    public float dashSpeed = 1.0f;
    public float dashCost;

    private float currentDashTime;
    private float savedWalkSpeed;
    private float savedSprintSpeed;

    private float jumpStore = 0;

    public GameObject playerModel;
    public GameObject playerExclaim;
    public Animator anim;
    public Transform pivot;
    public ParticleSystem sprintParticles;

    public AudioClip jumpClip;
    public AudioClip dashClip;
    public AudioClip cannotDashClip;

    public bool canMove = true;
    [HideInInspector]
    public bool canAttack = true;
    //[HideInInspector]
    public bool canDash = true;
    [HideInInspector]
    public bool exclaimed;

    [HideInInspector]
    public CharacterController controller;
    [HideInInspector]
    public Vector3 moveDirection;

    //Foster Stuff
    private bool jumpPad;
    private bool knockBack;
    private float timer;
    [HideInInspector]
    public float jumpPadHeight;

    [HideInInspector]
    public float curStam;
    [HideInInspector]
    public float maxStam;
    [HideInInspector]
    public float stamRegen;
    public bool isDashing;
    private bool isJumping;
    private bool jumpCheck;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        currentSpeed = walkSpeed;
        timer = 3;
        currentDashTime = maxDashTime;
        curStam = maxStam;

        savedWalkSpeed = walkSpeed;
        savedSprintSpeed = sprintSpeed;

        exclaimed = true;
    }

    private void Update()
    {
        MovementInputs();
        PFI();

        if (!exclaimed)
        {
            StartCoroutine(Exclaim());
            exclaimed = true;
        }
    }

    private void MovementInputs()
    {

        if (canMove)
        {
            float yStore = moveDirection.y;
            moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") * currentSpeed) + (transform.right * Input.GetAxisRaw("Horizontal"));
            moveDirection = moveDirection.normalized * currentSpeed;
            moveDirection.y = yStore;

            if (controller.isGrounded)
            {
                moveDirection.y = 0f;
                jumpStore = 1;

                if (knockBack)
                {
                    timer -= Time.deltaTime;
                    moveDirection.z = -5;
                    if (timer <= 0)
                    {
                        knockBack = false;
                        timer = 3;
                    }
                }                
            }

            if (jumpStore == 1)
            {
                isJumping = false;
                jumpCheck = false;

                if (Input.GetButtonDown("Jump") && TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
                {
                    moveDirection.y = jumpForce;
                    jumpStore = 0;

                    isJumping = true;

                    if (moveDirection.y > 0)
                    {
                        PlayerStamina.Instance.SubtractStamina(.2f);

                        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                        AudioManager.instance.PlayClip(jumpClip);
                    }

                    #region Foster's Jump Pad
                    /*if (jumpPad)
                    {
                        moveDirection.y = jumpPadHeight;
                    }
                    else
                    {
                        moveDirection.y = jumpForce;

                        //New

                        if(moveDirection.y > 0)
                        {
                            PlayerStamina.Instance.SubtractStamina(.2f);

                            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                            AudioManager.instance.PlayClip(jumpClip);
                        }
                    }*/
                    #endregion
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.A) &&
                !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.S) && TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
            {
                currentSpeed = sprintSpeed;
                sprintParticles.Play();
            }
            else
            {
                currentSpeed = walkSpeed;
                sprintParticles.Stop();
            }

            Physics.SyncTransforms();

            moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
            controller.Move(moveDirection * Time.deltaTime);

            //Move the player in different directions based on camera look direction
            if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
            {
                transform.rotation = Quaternion.Euler(0, pivot.rotation.eulerAngles.y, 0);
                Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
                playerModel.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
            }

            //Dash
            //Updated 8/13/2019
            if (TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
            {

                if ((Input.GetMouseButtonDown(1)) && PlayerStamina.Instance.stamina >= dashCost && canDash)
                {
                    if (-moveDirection.z > 0 || moveDirection.z > 0 || -moveDirection.x > 0 || moveDirection.x > 0)
                    {
                        StartCoroutine(Dash());
                    }

                    #region Old Method
                    /*if (curStam >= 1)
                    {
                        anim.SetBool("isDashing", true);
                        moveDirection = transform.forward * dashSpeed;
                        curStam -= 1;
                    }*/
                    #endregion
                }
                else
                {
                    anim.SetBool("isDashing", false);
                }


                if (Input.GetMouseButtonDown(1) && canDash == false) //|| PlayerStamina.Instance.stamina < dashCost)
                {
                    AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                    AudioManager.instance.PlayClip(cannotDashClip);
                }

                else
                {

                }
            }


            if (Input.GetMouseButtonDown(0) && TransformationController.Instance.type == TransformationController.TransformationType.BEAST)
            {
                anim.SetTrigger("beastAttack");
            }

            //anim.SetBool("isGrounded", controller.isGrounded);
        }
        
    }

    //New 8/13/2019
    IEnumerator Dash()
    {

        anim.SetBool("isDashing", true);
        walkSpeed = dashSpeed;
        sprintSpeed = dashSpeed;
        PlayerStamina.Instance.SubtractStamina(dashCost);

        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(dashClip);
        
        yield return new WaitForSeconds(maxDashTime);
        walkSpeed = savedWalkSpeed;
        sprintSpeed = savedSprintSpeed;
    }

    public void PFI()
    {
        if(!isJumping)
        {
            float wait = 0.1f;
            wait -= Time.deltaTime;

            if(wait == 0)
            {
                wait = 0.1f;
                sprintParticles.Play();
            }
            else
            {

            }
        }
    }

    public IEnumerator Exclaim()
    {
        playerExclaim.SetActive(true);
        AudioManager.instance.PlayClip(AudioManager.instance.exclaim);

        yield return new WaitForSeconds(1);

        playerExclaim.SetActive(false);
    }

    void OnTriggerEnter(Collider collisionInfo)
    {

        /*if (collisionInfo.gameObject.tag == "JumpPad")
        {
            jumpPad = true;
        }*/

        if (collisionInfo.gameObject.tag == "Tornado")
        {
            knockBack = true;
        }

        if (collisionInfo.gameObject.tag == "PoisonArea")
        {
            walkSpeed = 3;
        }

        if (collisionInfo.gameObject.tag == "Wind")
        {
            walkSpeed = 1;
        }
    }

    void OnTriggerExit(Collider collisionInfo1)
    {

        /*if (collisionInfo1.gameObject.tag == "JumpPad")
        {
            jumpPad = false;
        }*/

        if (collisionInfo1.gameObject.tag == "Tornado")
        {
            knockBack = false;
        }

        if (collisionInfo1.gameObject.tag == "PoisonArea")
        {
            walkSpeed = 12;
        }

        if (collisionInfo1.gameObject.tag == "Wind")
        {
            walkSpeed = 12;
        }
    }
}




#region Old Script

/*public float currentSpeed;
public float walkSpeed;
public float sprintSpeed;
public float jumpForce;
public float gravityScale;
public float rotateSpeed;

public GameObject playerModel;
public Animator anim;
public Transform pivot;

public bool canMove = true;
public bool canAttack = true;

private CharacterController controller;

private Vector3 moveDirection;

private void Start()
{
    controller = GetComponent<CharacterController>();
    walkSpeed = 2;
    sprintSpeed = 10;
    currentSpeed = walkSpeed;
}

private void Update()
{
    MovementInputs();
}

private void MovementInputs()
{
    if (canMove)
    {
        float yStore = moveDirection.y;
        moveDirection = (transform.forward * Input.GetAxisRaw("Vertical") * currentSpeed) + (transform.right * Input.GetAxisRaw("Horizontal"));
        moveDirection = moveDirection.normalized * currentSpeed;
        moveDirection.y = yStore;

        if (controller.isGrounded)
        {
            moveDirection.y = 0f;

            //When PlayerAbilities is renabled - && !playerAbilities.isGhost
            if (Input.GetButtonDown("Jump") && TransformationController.Instance.type != TransformationController.TransformationType.GHOST)
            {
                moveDirection.y = jumpForce;
            }
        }
        //When PlayerAbilities is Renabled - !playerAbilities.isGhost && !playerAbilities.isBeast &&
        if (Input.GetKey(KeyCode.LeftShift) && anim.GetBool("walkingBackwards") == false && !Input.GetKey(KeyCode.A) && 
            !Input.GetKey(KeyCode.D) && anim.GetFloat("speed") > 0 && anim.GetFloat("speed") >= 0 && 
            TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
        {
            currentSpeed = sprintSpeed;

            anim.SetBool("isRunning", true);

            if(anim.GetBool("isRunning") && anim.GetFloat("speed") <= 0)
            {
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            currentSpeed = walkSpeed;
            anim.SetBool("isRunning", false);
        }

        Physics.SyncTransforms();

        moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime);
        controller.Move(moveDirection * Time.deltaTime);

        //Move the player in different directions based on camera look direction
        if (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            transform.rotation = Quaternion.Euler(0, pivot.rotation.eulerAngles.y, 0);
            Quaternion newRotation = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.z));
            playerModel.transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            anim.SetBool("walkingBackwards", true);
            anim.SetBool("strafing", false);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = walkSpeed;
                anim.SetBool("isRunning", false);
            }

        }
       else if (Input.GetKeyDown(KeyCode.W))
        {
            anim.SetBool("walkingBackwards", false);
            anim.SetBool("strafing", false);
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            anim.SetBool("walkingBackwards", false);
        }
        else if (canAttack)
        {
            if (Input.GetMouseButtonDown(0) && TransformationController.Instance.type == TransformationController.TransformationType.BEAST)
            {
                anim.SetTrigger("isAttack");
            }
        }

        if(!Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            anim.SetBool("strafing", true);

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                currentSpeed = walkSpeed;
                anim.SetBool("isRunning", false);
            }

            if (Input.GetAxis("Horizontal") > 0)
            {
                anim.SetFloat("leftRight", 1);
            }
            else
            {
                anim.SetFloat("leftRight", 0);
            }
        }
        else if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("strafing", false);
        }

        anim.SetBool("isGrounded", controller.isGrounded);

        anim.SetFloat("speed", (Mathf.Abs(Input.GetAxis("Vertical")) + Mathf.Abs(Input.GetAxis("Horizontal"))));
    }
    //moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, Input.GetAxis("Vertical") * moveSpeed);

}*/
#endregion

