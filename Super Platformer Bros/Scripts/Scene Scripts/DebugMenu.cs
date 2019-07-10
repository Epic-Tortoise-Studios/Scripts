using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    public GameObject debugMenu;
    public GameObject playerUI;
    private GameObject player;
    private GameMaster gm;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public PlayerAbilities playerAbilities;

    public Text healthText;
    public Text movementText;
    public Text abilityText;
    public Text saveText;

    public static bool GameIsPaused = false;
    private Vector3 checkpointPOS;

    public void Awake()
    {
        playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>();

        playerMovement = GameObject.Find("Player").GetComponent<PlayerMovement>();
        playerAbilities = GameObject.Find("Player").GetComponent<PlayerAbilities>();

        player = GameObject.FindGameObjectWithTag("Player");
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        Stats();
        NoOtherMenus();
    }

    void Resume()
    {
        debugMenu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        debugMenu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    
    public void GodModeOn() //Phenominal Cosmic Powa!
    {
        if(playerHealth.isGod == false)
        {
            playerHealth.isGod = true;
            playerHealth.superSaiyanOn = true;
        }
    }

    public void GodModeOff() //itty bitty living space
    {
        if (playerHealth.isGod == true)
        {
            playerHealth.isGod = false;
            playerHealth.superSaiyanOn = false;
        }
    }

    public void HittingYourself() //Why are you?
    {
        playerHealth.DebugDamage();
    }
    
    public void Lamb() //As in Sacrificial
    {
        //Kat: Changed this to setting health to zero instead of destroying the player
        playerHealth.currentHealth = 0;
    }

    public void Manhattan() //Dr.
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
            GameObject.Destroy(enemy);
    }

    public void CheckPlease() //They don't all need assistance
    {

    }

    public void TimeWarp() //Let's do it again...
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Resume();
    }

    public void Checkpoint1()
    {
        /*CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        player.transform.position = gm.C1;
        controller.enabled = true;*/

        Debug.Log("C1");

        player.transform.position = gm.checkpoint1.transform.position;
        
    }
    public void Checkpoint2()
    {
        /*CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false; 
        player.transform.position = gm.C2;
        controller.enabled = true;*/
        player.transform.position = gm.checkpoint2.transform.position;

        Debug.Log("C2");
    }
    public void Checkpoint3()
    {
        player.transform.position = gm.checkpoint3.transform.position;

        Debug.Log("C3");
    }
    public void Checkpoint4()
    {
        player.transform.position = gm.checkpoint4.transform.position;

        Debug.Log("C4");
    }
    public void Checkpoint5()
    {
        player.transform.position = gm.checkpoint5.transform.position;

        Debug.Log("C5");
    }
    public void Checkpoint6()
    {
        player.transform.position = gm.checkpoint6.transform.position;

        Debug.Log("C6");
    }
    public void Checkpoint7()
    {
        player.transform.position = gm.checkpoint7.transform.position;

        Debug.Log("C7");
    }
    public void Checkpoint8()
    {
        player.transform.position = gm.checkpoint8.transform.position;

        Debug.Log("C8");
    }

    public void Stats()
    {
        healthText.text = ("Max Health: " + playerHealth.maxHealth + "\nHealth: " + playerHealth.currentHealth + "\nPlayer Dead: " + playerHealth.playerDead + "\nGod Mode: " + 
            playerHealth.isGod);

        movementText.text = ("Speed: " + playerMovement.moveSpeed + "\nJump Force: " + playerMovement.jumpForce +
            "\nSecond Jump Force: " + playerMovement.secondJumpForce + "\nPlayer Inputs: " + playerMovement.playerInputs);

        abilityText.text = ("Jump Height: " + playerAbilities.jumpHeight + "\nMax Jump Height: " + playerAbilities.maxHeight + "\nInvul Time: " + playerAbilities.invulTime
            + "\nSuper Jump Height: " + playerAbilities.superJumpHeight + "\nSuper Speed: " + playerAbilities.superSpeedValue + "\nShoot Power: " + playerAbilities.shootPower
            + "\nJump Power: " + playerAbilities.jumpPower + "\nSpeed Power: " + playerAbilities.speedPower + "\nInvul Power: " + playerAbilities.invulnerablePower);

        checkpointPOS = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), PlayerPrefs.GetFloat("ZPosition"));

        saveText.text = ("Saved Health: " + PlayerPrefs.GetInt("Player Health") + "\nLevel: " + PlayerPrefs.GetInt("Level Number") + "\nCheckpoint Position: " + checkpointPOS);
    }

    public void NoOtherMenus()
    {
        if(GameIsPaused)
        {
            playerUI.SetActive(false);
        }
        else
        {
            playerUI.SetActive(true);
        }
    }
}
