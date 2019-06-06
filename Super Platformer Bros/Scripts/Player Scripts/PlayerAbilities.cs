using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilities : MonoBehaviour
{
    private GameObject player;
    private PlayerMovement movement;
    public GameObject shootLocation;

    public float jumpHeight;
    public float maxHeight;
    public float invulTime;
    public float superJumpHeight;
    public float superSpeedValue;

    public bool shootPower = false;
    public bool jumpPower = false;
    public bool speedPower = false;
    public bool invulnerablePower = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movement = GetComponent<PlayerMovement>();
    }


    void Update()
    {
        ShootAbility();
        JumpAbility();
        SpeedAbility();
        InvulnerableAbility();
    }

    void ShootAbility()
    {
        if (shootPower == true)
        {
            shootLocation.SetActive(true);
            Debug.Log("Shoot Power: " + shootPower);
            jumpPower = false;
            speedPower = false;
        }
        else if (shootPower == false)
        {
            shootLocation.SetActive(false);
        }
    }

    void JumpAbility()
    {
        if ((player.GetComponent<PlayerMovement>().jumpForce == jumpHeight) && (jumpPower == true))
        {
            //player.GetComponent<PlayerMovement>().jumpForce *= 2; // Multiplies the jumpForce by 2
            player.GetComponent<PlayerMovement>().jumpForce = superJumpHeight;

            Debug.Log(player.GetComponent<PlayerMovement>().jumpForce);

            shootPower = false;
            speedPower = false;
        }
        else if ((player.GetComponent<PlayerMovement>().jumpForce > jumpHeight) && (jumpPower == true))
        {
            player.GetComponent<PlayerMovement>().jumpForce = maxHeight; // If using a double jump, this forces the player to stop at the maximum elevation.
        }
        else if(jumpPower == false)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().jumpForce = 20;
        }

    }

    void SpeedAbility()
    {
        if(speedPower == true)
        {
            //movement.moveSpeed = 20;
            movement.moveSpeed = superSpeedValue;
            jumpPower = false;
            shootPower = false;
        }

        else if(speedPower == false)
        {
            movement.moveSpeed = movement.baseMoveSpeed;
        }
    }

    void InvulnerableAbility()
    {
        if(invulnerablePower == true)
        {
            player.GetComponent<PlayerHealth>().superSaiyanOn = true;
            player.GetComponent<PlayerHealth>().isGod = true;
            StartCoroutine(InvulnerabilityTimer());
        }
    }

    IEnumerator InvulnerabilityTimer()
    {
        yield return new WaitForSeconds(invulTime);
        player.GetComponent<PlayerHealth>().superSaiyanOn = false;
        player.GetComponent<PlayerHealth>().isGod = false;
        player.GetComponent<PlayerHealth>().currentHealth = 100;
        invulnerablePower = false;
        player.GetComponent<PlayerHealth>().currentHealth = 100;
        Destroy(GameObject.FindWithTag("SuperSaiyan"));
    }
}
