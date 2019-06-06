using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTrap : MonoBehaviour
{
    public GameObject currentPlayer;
    private PlayerMovement playerMovement;
    private PlayerAbilities playerAbilities;
    private PlayerHealth playerHealth;
    private float originalSpeed;
    private float newSpeed;

    private float originalSprintSpeed;
    private float newSprintSpeed;

    public bool resetSpeed = false;

    public bool hadSpeedBoost = false;

    private float originalJumpForce; // Height of the first jump
    private float originalSecondJumpForce; // The height of the second jump
    private float originalMinJumpForce;
    private float newJumpForce; // Height of the first jump
    private float newSecondJumpForce; // The height of the second jump
    private float newMinJumpForce; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerMovement = currentPlayer.GetComponent<PlayerMovement>();
        playerAbilities = currentPlayer.GetComponent<PlayerAbilities>();
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
        
        if (playerHealth.playerDead == true)
        {
            resetSpeed = true;
            ResetSpeed();
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerAbilities.speedPower == true)
        {
            hadSpeedBoost = true;

            if (hadSpeedBoost == true)
            {
                playerAbilities.speedPower = false;

                originalSpeed = playerMovement.baseMoveSpeed;
                originalJumpForce = playerMovement.jumpForce;
                originalSecondJumpForce = playerMovement.secondJumpForce;
                originalMinJumpForce = playerMovement.minJumpForce;
                originalSprintSpeed = playerMovement.sprintSpeed;

                newSpeed = originalSpeed / 3;
                newJumpForce = originalJumpForce / 2;
                newSecondJumpForce = originalSecondJumpForce / 3;
                newMinJumpForce = originalMinJumpForce / 3;
                newSprintSpeed = originalSprintSpeed - originalSprintSpeed;

                playerMovement.baseMoveSpeed = newSpeed;
                playerMovement.jumpForce = newJumpForce;
                playerMovement.secondJumpForce = newSecondJumpForce;
                playerMovement.minJumpForce = newMinJumpForce;
                playerMovement.sprintSpeed = newSprintSpeed;

                Debug.Log("slow player");
                resetSpeed = true;
            }
            
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            originalSpeed = playerMovement.baseMoveSpeed;
            originalJumpForce = playerMovement.jumpForce;
            originalSecondJumpForce = playerMovement.secondJumpForce;
            originalMinJumpForce = playerMovement.minJumpForce;
            originalSprintSpeed = playerMovement.sprintSpeed;

            newSpeed = originalSpeed / 3;
            newJumpForce = originalJumpForce / 2;
            newSecondJumpForce = originalSecondJumpForce / 3;
            newMinJumpForce = originalMinJumpForce / 3;
            newSprintSpeed = originalSprintSpeed - originalSprintSpeed;

            playerMovement.baseMoveSpeed = newSpeed;
            playerMovement.jumpForce = newJumpForce;
            playerMovement.secondJumpForce = newSecondJumpForce;
            playerMovement.minJumpForce = newMinJumpForce;
            playerMovement.sprintSpeed = newSprintSpeed;

            Debug.Log("slow player");
            resetSpeed = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        ResetSpeed();
        
    }

    void ResetSpeed()
    {
        if (hadSpeedBoost == true && resetSpeed == true)
        {
            playerAbilities.speedPower = true;

            playerMovement.baseMoveSpeed = originalSpeed;
            playerMovement.jumpForce = originalJumpForce;
            playerMovement.secondJumpForce = originalSecondJumpForce;
            playerMovement.minJumpForce = originalMinJumpForce;
            playerMovement.sprintSpeed = originalSprintSpeed;

            hadSpeedBoost = false;
            resetSpeed = false;
        }
        else if (resetSpeed == true)
        {
            playerMovement.baseMoveSpeed = originalSpeed;
            playerMovement.jumpForce = originalJumpForce;
            playerMovement.secondJumpForce = originalSecondJumpForce;
            playerMovement.minJumpForce = originalMinJumpForce;
            playerMovement.sprintSpeed = originalSprintSpeed;

            resetSpeed = false;
        }
    }

}
