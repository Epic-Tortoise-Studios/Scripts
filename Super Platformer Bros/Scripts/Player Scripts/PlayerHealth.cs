using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    public bool playerDead = false;

    private float respawnTimer = 2f;

    //Kat: For the Body Stepping mechanic. Considering Placing this in it's own script but this works for now.
    private bool bodyStepping = true;
    private bool spawnBody;
    private Vector3 deathSpot;
    public GameObject deadPlayerPrefab;

    private Animator anim;

    private GameMaster gm;
    private HookManager hookManager;
    private PlayerAbilities playerAbilities;
    private PlayerMovement playerMovement;

    //Scripts for GodMode
    public bool superSaiyanOn = false;
    public bool isGod = false;
    private bool isSuperSaiyan = false;
    public GameObject superSaiyan;

    private GameObject saveManager;
    private SavePrefs savePrefs;
    
    public DebugMenu debugMenuScript;
    public EnemySpawnTrigger enemyspawnTrigger;

    private void Start()
    {
        //Kat: Commenting this out for Save File
        currentHealth = maxHealth;  //Sets current HP to whatever Max HP is in inspector
        debugMenuScript = GameObject.FindGameObjectWithTag("DebugMenu").GetComponent<DebugMenu>();
        enemyspawnTrigger = GameObject.FindGameObjectWithTag("EnemySpawn").GetComponent<EnemySpawnTrigger>();
        anim = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        hookManager = this.gameObject.GetComponent<HookManager>();
        playerAbilities = this.gameObject.GetComponent<PlayerAbilities>();

        playerMovement = this.gameObject.GetComponent<PlayerMovement>();

        //Kat: This sets the players current health to whatever was saved last. Can easily be commented out to set to max health if preffered.
        /*saveManager = GameObject.FindGameObjectWithTag("SaveManager");
        savePrefs = saveManager.GetComponent<SavePrefs>();
        currentHealth = PlayerPrefs.GetInt("Player Health");

        if(currentHealth <= 0)
        {
            currentHealth = maxHealth;

            Debug.Log("Kat Check");
        }*/

    }
    

    private void Update()
    {
        CheckHealth();
        CheckGod();
        //CheckHook();
        RespawnPlayer();
        BodyStepping();
    
    }

    void CheckHealth()
    {
       if (isGod || superSaiyanOn == true)
        {
            if (isSuperSaiyan == false)
            {
                Instantiate(superSaiyan, transform);
                isSuperSaiyan = true;

                if (currentHealth <= 99)
                {
                    currentHealth = 100;
                }
            }
        }
        else if (currentHealth <= 0 && playerMovement.controller.isGrounded) //Kat: I added the ground check to stop the player from dying in the air
        {
            playerDead = true;
            deathSpot = this.transform.position;
            //anim.SetBool("isDead", true);
            currentHealth = 0;
        }
        else if(currentHealth > 0)
        {
            //playerDead = false;
            anim.SetBool("isDead", false);
        }
    }

    //Kat: This is for player respawning. Can be switched to another script to clean up health if we need to?
    public void RespawnPlayer()
    {
        if (playerDead)
        {
            respawnTimer -= Time.deltaTime;
            anim.SetBool("isDead", true);
            debugMenuScript.Manhattan();
            enemyspawnTrigger.ResetSpawn();


            if (respawnTimer <= 0)
            {
                respawnTimer = 2;
                if (bodyStepping)
                {
                    spawnBody = true;
                }
                this.gameObject.transform.position = gm.playerLastPos;
                ResetHealth();
                anim.SetBool("isWalking", false);
                playerDead = false;
            }
        }
        else if(playerDead == false)
        {
            anim.SetBool("isDead", false);
        }
    }

    //Kat: Resets health back to to max. Chris recommeneded keeping this in a seperate function.
    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    //Kat: This function is for our Hook 3 mechanic. Can also be moved to a new script but functions fine in the Health for now.
    public void BodyStepping()
    {
        if (bodyStepping)
        {
            if (spawnBody)
            {
                Instantiate(deadPlayerPrefab, deathSpot, deadPlayerPrefab.transform.rotation);
                Debug.Log("Spawn Body At: " + deathSpot);
                spawnBody = false;
            }
        }
    }

    //Kat: I made a new Hook Manager script that detects what hook the player is in by a bool check. The hookmanager is currently on the player themselves.

    //Kat Update: Because we're combining our hooks, this can be removed to spawn bodies throughout the level
    /*public void CheckHook()
    {
        if (hookManager.inHook3)
        {
            bodyStepping = true;
            Debug.Log("In Hook 3");
            playerAbilities.jumpPower = false;
            playerAbilities.speedPower = false;
            playerAbilities.shootPower = false;
            playerAbilities.invulnerablePower = false;
        }
        else if(hookManager.inHook3 == false)
        {
            bodyStepping = false;
        }
    }*/

    //Kat: For Debug Menu stuff
    public void DebugDamage()
    {
        currentHealth -= 10;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        /*if (currentHealth <= 0)
        {
            currentHealth = 0;
            Destroy(gameObject);
        }*/
    }

    public void CheckGod()
    {
        if (superSaiyanOn == false)
        {
            Destroy(GameObject.FindWithTag("SuperSaiyan"));
        }
    }

    public void OnHealthPickup(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
    }



    //Kat: This is part of the old PlayerHealth script. All of these damage dealing objects are now dealing damage inside of their own scripts.

    //public GameObject deadPlayer;
    //public GameObject ghost;

    //public int goombaDamage;
    //public int fireBall;

    /*void OnTriggerEnter(Collider other)
    {
        if(other.name == "FireBall(Clone)")  //Needs (Clone) because thats exact name
        {
            ApplyDamage(fireBall);  //Calls the apply damage function
        }
        else
            if(other.name == "SpikeTrigger")
        {
            SpikeDamage();
        }
        else 
            if(other.name == "Ceiling")
        {
            InstaDeath();
        }
        else 
            if(other.name == "Floor")
        {
            InstaDeath();
        }

        //Damage for Goomba to player if hit from side
        else 
            if(other.name == "GoombaLeft")
        {
            ApplyDamage(goombaDamage);
        }
        else 
            if(other.name == "GoombaRight")
        {
            ApplyDamage(goombaDamage);
        }
    }*/


    /*public void ApplyDamage(int amount)
    {
        //knocks 1pt of health off player
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);  //Makes it so health can't drop below 0 and bug it out

        if(currentHealth <=0)
        {
            currentHealth = 0;  //Extra precaution HP doesnt go below 0 and bug it out
            //Instantiate(deadPlayer, transform.position + (transform.forward * 1), transform.rotation); //Makes the Corpse
            //Instantiate(ghost, transform.position + (transform.forward * 2), transform.rotation);
            Destroy(gameObject); //Destroy Player Avatar

        }
    }*/

    //Call this function anytime you want something to kill the player on contact 
    public void InstaDeath()
        {
            Destroy(gameObject);
            //Instantiate(deadPlayer, transform.position + (transform.forward * 1), transform.rotation);
            //Instantiate(ghost, transform.position + (transform.forward * 2), transform.rotation);
        }

    /*
   //Death by SpikeTrap
   public void SpikeDamage()
   {
       Destroy(gameObject);
       //Instantiate(deadPlayer, transform.position + (transform.forward * 1), transform.rotation);
      // Instantiate(ghost, transform.position + (transform.forward * 2), transform.rotation);
   }*/

    /*public void GoombaDamage(int amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Instantiate(deadPlayer, transform.position + (transform.forward * 1), transform.rotation); //Makes the Corpse
            Instantiate(ghost, transform.position + (transform.forward * 2), transform.rotation);

        }

    }*/

}