using System.Collections;
using UnityEngine;

public class UserMovement : MonoBehaviour
{

    public float jumpSpeed = 30.0f;
    public float gravity = 55.0f;
    public float runSpeed = 70.0f;
    public float runSpeed1 = 70.0f;
    public float runSpeed2 = 140.0f;
    private float walkSpeed = 90.0f;
    private float rotateSpeed = 150.0f;

    public bool grounded;
    private Vector3 moveDirection = Vector3.zero;
    private bool isWalking;
    private string moveStatus = "idle";

    public GameObject camera1;
    public CharacterController controller;
    public bool isJumping;
    private float myAng = 0.0f;
    public bool canJump = true;

    void Start()
    {

        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        //force controller down slope. Disable jumping
        if (myAng > 50)
        {

            canJump = false;
        }
        else
        {

            canJump = true;
        }

        if (grounded)
        {

            isJumping = false;

            if (camera1.transform.gameObject.transform.GetComponent<UserCamera>().inFirstPerson == true)
            {
                moveDirection = new Vector3((Input.GetMouseButton(0) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));
            }
            moveDirection = new Vector3((Input.GetMouseButton(1) ? Input.GetAxis("Horizontal") : 0), 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= isWalking ? walkSpeed : runSpeed;

            moveStatus = "idle";



            if (moveDirection != Vector3.zero)
                moveStatus = isWalking ? "walking" : "running";

            if (Input.GetKeyDown(KeyCode.Space) && canJump)
            {
                moveDirection.y = jumpSpeed;
                isJumping = true;
            }

        }


        // Allow turning at anytime. Keep the character facing in the same direction as the Camera if the right mouse button is down.

        if (camera1.transform.gameObject.transform.GetComponent<UserCamera>().inFirstPerson == false)
        {
            if (Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }
            else
            {
                transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed * Time.deltaTime, 0);

            }
        }
        else
        {
            if (Input.GetMouseButton(0) || Input.GetMouseButton(1))
            {
                transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
            }

        }

        //Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;


        //Move controller
        CollisionFlags flags;
        if (isJumping)
        {
            flags = controller.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            flags = controller.Move((moveDirection + new Vector3(0, -100, 0)) * Time.deltaTime);
        }

        grounded = (flags & CollisionFlags.Below) != 0;

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {

        myAng = Vector3.Angle(Vector3.up, hit.normal);
    }
}