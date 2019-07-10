using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed; // The player movement speed
    public float baseMoveSpeed;
    public float jumpForce; // Height of the first jump
    public float secondJumpForce; // The height of the second jump
    public float minJumpForce;
    public float sprintSpeed;
    public float force; //This is for KnockBack Trigger
    public float slowSpeed;
    public bool isJumping;
    public CharacterController controller; //Kat: I made this public so I could call it in the PlayerHealth
    
    private Animator anim;

    private Vector3 moveDirection;
    public float gravityScale;
    public float groundPoundGravity;
    public float jumpFallGravity;
    //[SerializeField]
    int maxJumps = 1;
    int jumpCounter = 0;

    public bool playerInputs = true;

    private PlayerHealth playerHealth;

    public AudioClip playerJumpSound;
    public AudioClip playerDoubleJumpSound;
    //public AudioClip playerWalkSound;
    public AudioSource audioSrc;

    //Kat: For Ground Pound Body Destruction
    public bool groundPounded;

    private void Start()
    {
        audioSrc = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        PlayerInputs();
        CheckHealth();
        if(controller.isGrounded == true)
        {
            moveDirection.y = (0.0f);
        }
        moveSpeed = baseMoveSpeed;
    }

    void PlayerInputs()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            moveSpeed = sprintSpeed;
        }
        if (playerInputs)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal") * moveSpeed, moveDirection.y, 0); //Checks movement input and direction
            
            //Kat: Variables for animations
            float moveX;
            bool isWalking;

            //Kat: Newly added for the Jump animation
            anim.SetBool("isJumping", isJumping);

            if (Input.GetKeyDown("space")) //Input for Jumping. GetKey runs each frame, continually checking if the input is triggered.
            {
                isJumping = true;


                if (controller.isGrounded) //If player is on the ground.
                {                    
                    moveDirection.y = 0f; //Player isn't moving upward.
                    jumpCounter = 0; //Jumps are set at zero.

                    

                    {
                        moveDirection.y = jumpForce; //When key is pressed, player will jump.
                        Debug.Log("Player is holding the jump button");
                        audioSrc.PlayOneShot(playerJumpSound);
                    }                    
                }
                //if (isJumping == true && Input.GetKeyUp(KeyCode.Space))
                //{
                //if (moveDirection.y > minJumpForce)
                //moveDirection.y = minJumpForce;
                //}
                /*else
                {
                    if (moveDirection.y > minJumpForce)
                        moveDirection.y = minJumpForce;
                    Debug.Log("Player Let go of jump button");
                }*/

            }
            if (Input.GetKeyUp("space"))
            {
                isJumping = false;
                if (isJumping == false)
                {
                    moveDirection.y = moveDirection.y + (Physics.gravity.y * jumpFallGravity * Time.deltaTime);
                }
            }

            if (!controller.isGrounded && jumpCounter < maxJumps) //If player is not on the ground and they haven't reached maximum jumps.
            {
                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime); //Applies gravity.

                if (Input.GetKeyDown(KeyCode.Space)) 
                {
                   

                    moveDirection.y = secondJumpForce; //When jump button is pressed again, a second jump force is applied. Creating the double jump.
                    //Debug.Log(jumpForce);
                    audioSrc.PlayOneShot(playerDoubleJumpSound);
                    jumpCounter++; //Increases the jump counter.
                }
            }
            else if (!controller.isGrounded && Input.GetKeyDown(KeyCode.S)) //If the player is already in the air and they press the "S" key, they will cancel their jump.
            {
                moveDirection.y = moveDirection.y + (Physics.gravity.y * groundPoundGravity * Time.deltaTime); //Applies the gravity for a ground pound. This is a variable that is larger than normal gravity to make the player descend quicker.
                //Kat: New
                groundPounded = true;
                Debug.Log("Player crashed to the ground.(Ground Pound)");              
            }

            //Kat: This is for the animations 
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                isWalking = true;
                moveX = Input.GetAxisRaw("Horizontal");
                anim.SetFloat("moveX", moveX);
                anim.SetBool("isWalking", isWalking);

                //audioSrc.PlayOneShot(playerWalkSound);

                if (anim.GetFloat("moveX") == 0)
                {
                    anim.SetFloat("moveX", 1f);
                }

               
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                isWalking = false;
                anim.SetBool("isWalking", isWalking);

                //audioSrc.PlayOneShot(playerWalkSound);
            }

                moveDirection.y = moveDirection.y + (Physics.gravity.y * gravityScale * Time.deltaTime); //Applies normal gravity for jumping.
                Physics.SyncTransforms();
                controller.Move(moveDirection * Time.deltaTime);                
        }
    }
    

    void CheckHealth()
    {
        if (playerHealth.playerDead)
        {
            playerInputs = false;
        }
        else if(playerHealth.playerDead == false)
        {
            playerInputs = true;
        }
    }

    //Below is for the KnockBack Trigger
    public void Launch()
    {
        transform.Translate(Vector3.left * force * Time.deltaTime);
        transform.Translate(Vector3.up * force * Time.deltaTime);

    }



    /* Kat: Old bits?
     if (Input.GetButtonDown("Jump"))
        {
            
            if (controller.isGrounded)
            {
                
                moveDirection.y = jumpForce;
                jumpCounter = 0;
            }
            

            if (!controller.isGrounded && jumpCounter < maxJumps)
            {
                moveDirection.y = jumpForce;
                jumpCounter++;
            }


    float moveX = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("moveX", moveX);
        bool isWalking = (moveX > 0);
        anim.SetBool("isWalking", isWalking);
        }*/



    /*
    //Chance: This is Kat's version.
    //Movement
    public float speed;
    public float jump;
    public float sprint;
    float moveVelocity;

    //Grounded Var 
    //Kat: Took the = true off this variable.
    [SerializeField]
    bool grounded;

    //Kat: This is for multiple jumping. 
    private int numOfJumps = 1;
    private int maxJumps = 2;

    void Update()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            if (grounded || numOfJumps < maxJumps)
            {
                GetComponent<Rigidbody>().velocity = new Vector2(GetComponent<Rigidbody>().velocity.x, jump);
                numOfJumps += 2;
            }
        }

        moveVelocity = 0;

        //Left Right Movement && Sprint
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            moveVelocity = -speed;
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
        {
            moveVelocity = -sprint;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            moveVelocity = speed;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
        {
            moveVelocity = sprint;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(moveVelocity, GetComponent<Rigidbody>().velocity.y);

    }


    //Kat: Changed jumping to OnCollision instead OnTrigger
    private void OnCollisionStay(Collision collider)
    {
        if (collider.gameObject.CompareTag("Ground") || collider.gameObject.CompareTag("BreakablePlatform"))
        {
            grounded = true;
            numOfJumps = 1;
        }
    }

    private void OnCollisionExit(Collision collider)
    {
        grounded = false;
    }*/

    /*Kat: This is Chris' original jump script
     * 
    //Check if Grounded so only jump once (Can add a double jump ez if wanted)
    //There needs to be a trigger volume of the same name as below to allow a jump
    //Add on as needed same way as below
    void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ground")
        {
            grounded = true;
        }
        else if (other.name == "Platform")
        {
            grounded = true;
        }
        else if (other.name == "DestructiblePlatform")
        {
            grounded = true;
        }
    }
    void OnTriggerExit()
    {
        grounded = false;
    }*/
}

