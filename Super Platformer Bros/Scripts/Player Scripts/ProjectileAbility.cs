using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileAbility : MonoBehaviour
{
    private PlayerAbilities playerAbilities;
    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = player.GetComponent<PlayerAbilities>();
    }

    private void Update()
    {
        //ShootAbility();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerAbilities.shootPower = true;
            playerAbilities.jumpPower = false;
        }
    }

    //public SuperJumpAbility superJumpAbility;

    //public PlayerMovement Player;
    //public PlayerMovement jumpHeight;
    //public GameObject shootLocation;
    //public bool shootPower = false;

    //Checking that player has killed and left enemy trigger volume
    /*void OnTriggerExit(Collider other)
    {
        shootPower = false;
    }*/

    //Function that gives player ability to shoot projectiles
    /*void ShootAbility()
    {
        if (shootPower == true)
        {
            //Kat: Original script
            shootLocation.SetActive(true); // Sets the shootLocation gameObject as active. Starts deactivated.
            Debug.Log("You picked up the gun.");
            //Kat: My script
            //shootLocation.GetComponent<playerShoot>().enabled = true;
            GameObject.Find("Player").GetComponent<PlayerMovement>().jumpForce = 20; // Returns the players jump height to base.
            //Debug.Log(jumpHeight.jump);

            superJumpAbility.superJumpAbility = false;
        }
        else if (shootPower == false)
        {
            //Kat: Original script
            shootLocation.SetActive(false); // Sets the players shootLocation gameObject to inactive.
            //Kat: My script
            //shootLocation.GetComponent<playerShoot>().enabled = false;
        }
    }*/

    //Rob's Original Code
    /*public PlayerMovement ShootAbility;
    public bool projectilePower = false;

    public GameObject thePlayer;

    public GameObject fireBall;
    public float fireTimer;
    private bool shotReady;

    // Start is called before the first frame update
    void Start()
    {
        shotReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        ProjectilePowerUp();

        if (shotReady)
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(fireBall, transform.position + (transform.forward * 4), transform.rotation);
        shotReady = false;
        StartCoroutine(FireRate());
    }

    IEnumerator FireRate()
    {
        yield return new WaitForSeconds(fireTimer);
        shotReady = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            projectilePower = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        projectilePower = false;
    }

    void ProjectilePowerUp()
    {
        if (projectilePower == true)
        {
            GameObject.Find("Player").GetComponent<PlayerMovement>().jump = 5;
        }
    }*/
}
