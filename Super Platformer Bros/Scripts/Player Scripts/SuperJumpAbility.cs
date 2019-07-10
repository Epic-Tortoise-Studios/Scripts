using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperJumpAbility : MonoBehaviour
{

    private PlayerAbilities playerAbilities;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
    }

    void Update()
    {
        //JumpAbility();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerAbilities.shootPower = false;
            playerAbilities.jumpPower = true;
        }
    }


    //public ProjectileAbility projectileAbility;

    //public CharacterController controller;
    //public bool superJumpAbility = false; // If Player has the Super Jump ability activated.
    //public float jumpHeight; // The players base jump height. Normally the same variable as the jumpForce from Character Controller.
    //public float maxHeight; // The players maximum jump height. This can be adjusted to represent maximum elevation.

    //public GameObject disableShootLocation;

    //Checking for player to leave the trigger volume
    /*void OnTriggerExit(Collider other)
    {
        superJump = false;
    }*/

    //If player kills the SuperJumpEnemy, code will take the jump value from 
    //      the PlayerMovement script and change it to make the player jump higher.
    //   **This is also where we would cancel the projectile ability.
    /*void JumpAbility()
    {
        if ((player.GetComponent<PlayerMovement>().jumpForce == jumpHeight) && (superJumpAbility == true))
        {
            player.GetComponent<PlayerMovement>().jumpForce *= 2; // Multiplies the jumpForce by 2

            Debug.Log(player.GetComponent<PlayerMovement>().jumpForce);

            superJumpAbility = true;
            projectileAbility.shootPower = false;
            disableShootLocation.SetActive(false);
            
        }
        else if ((player.GetComponent<PlayerMovement>().jumpForce > jumpHeight) && (superJumpAbility == true))
        {
            player.GetComponent<PlayerMovement>().jumpForce = maxHeight; // If using a double jump, this forces the player to stop at the maximum elevation.

            superJumpAbility = false;
        }
        
    }*/
}
