using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilities : MonoBehaviour
{

    #region Sigleton
    private static PlayerAbilities instance;
    public static PlayerAbilities Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerAbilities>();
            return instance;
        }
    }
    #endregion

    private PlayerController playerController;
    private CameraEffects cameraEffects;

    //private bool hasMeleeCompanion;
    //private bool hasHealerCompanion;
    //private MeleeMovement meleeMovement;
    //private HealerMovement healerMovement;

    public Animator anim;
    private Animator savedAnim;
    private bool moveForAnimation;

    public Transform bodySpawnPosition;

    public float cooldown = 10;
    public GameObject ghostPanel;
    public Image abilityImage;
    public Image cooldownImage;
    public TMPro.TextMeshProUGUI abilityText;

    public Sprite normalSprite;
    public Sprite ghostSprite;
    public Sprite possessedSprite;
    public Sprite beastSprite;

    public GameObject playerRig;
    public GameObject beastRig;
    public GameObject beastFist;
    public GameObject playerRigJoints;
    public GameObject playerRigSurface;
    public GameObject playerWeapon;
    public GameObject bodyPrefab;
    public GameObject droppedBody;
    public GameObject transformPosition;
    public GameObject possessableEnemy;

    //public AudioClip deathBell;
    public AudioClip spiritForm;
    public AudioClip spiritDeath;

    public bool isGhost;
    public bool isBeast;
    public bool isCooldown;
    public bool possessedEnemy;

    public bool speedAbility;
    private float savedSprintSpeed;
    private float savedWalkSpeed;

    public LayerMask layerMask;


    void Start()
    {
        playerController = gameObject.GetComponent<PlayerController>();

        //meleeMovement = GameObject.FindGameObjectWithTag("MeleeCompanion").GetComponent<MeleeMovement>();
        //healerMovement = GameObject.FindGameObjectWithTag("HealthCompanion").GetComponent<HealerMovement>();

        cameraEffects = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraEffects>();
        anim = gameObject.GetComponentInChildren<Animator>();

        savedSprintSpeed = playerController.sprintSpeed;
        savedWalkSpeed = playerController.walkSpeed;
        savedAnim = playerController.anim;
        beastFist.SetActive(false);
        
    }


    void FixedUpdate()
    {
        PlayerInput();
        TransformingAbilities();
        PossessEnemy();
        EnemySpeedAbility();

        /*if (meleeMovement.isActive)
        {
            hasMeleeCompanion = true;
            hasHealerCompanion = false;
        }

        if (healerMovement.isActive)
        {
            hasHealerCompanion = true;
            hasMeleeCompanion = false;
        }*/
    }

    //Ghost function currently acts on button press, behavior can be added later to change this
    void PlayerInput()
    {
        //Turn Ghost //!PlayerStats.Instance.isRespawning
        if (Input.GetKeyDown(KeyCode.G) && !isGhost && !possessedEnemy && !isBeast)
        {
            isGhost = true;
            PlayerHealth.Instance.TakeDamage(.5f);
            anim.SetBool("isDead", true);
            //playerController.canMove = false;

            gameObject.layer = 12;
            Physics.IgnoreLayerCollision(12, 13, true);

            if (droppedBody == null)
            {
                StartCoroutine(DeathBehavior());
            }
            else
            {
                Debug.Log("Body Already Instantiated");               
            }
        }
        else if (Input.GetKeyDown(KeyCode.H))
        {
            if(isGhost && !possessedEnemy)
            {
                isGhost = false;
                anim.SetBool("isDead", false);
                anim.SetBool("resurrect", true);

                gameObject.layer = 0;
                Physics.IgnoreLayerCollision(12, 13, false);

                if (droppedBody != null)
                {
                    StartCoroutine(ResurrectionBehavior());
                }
            }
            else if (isBeast && !isGhost && !possessedEnemy && !Input.GetKeyDown(KeyCode.LeftShift))
            {
                layerMask = 0;
                isBeast = false;
                Debug.Log("Is Not Beast, Is Not Ghost");
                playerController.anim = savedAnim;
                beastRig.SetActive(false);
                playerRig.SetActive(true);
            }
        }

        //Turn Beast
        if (Input.GetKeyDown(KeyCode.J) && !isGhost && !possessedEnemy)
        {
            //StartCoroutine(TurnBeast());
            layerMask = 14;
            isBeast = true;
            playerController.anim = beastRig.GetComponent<Animator>();
            beastRig.SetActive(true);
            playerRig.SetActive(false);
            anim.SetBool("turnBeast", true);
            Debug.Log("Is Beast");
            
        }
    }

    //Abilities Given By Items   

    public void TransformingAbilities()
    {
        if (isGhost)
        {
            anim.SetBool("isGhost", true);
            gameObject.layer = 12;

            abilityImage.sprite = ghostSprite;
            abilityText.text = "Incorporeal";
            /*if (hasMeleeCompanion)
            {
                meleeMovement.wait = true;
                meleeMovement.follow = false;
            }

            if (hasHealerCompanion)
            {
                healerMovement.wait = true;
                healerMovement.follow = false;
            }*/

            if (PlayerHealth.Instance.Health <= 0)
            {

            }
        }
        else if (!isGhost || possessedEnemy)
        {
            anim.SetBool("isGhost", false);
            gameObject.layer = 0;
            Physics.IgnoreLayerCollision(12, 13, false);
        }

        if (isBeast)
        {
            abilityImage.sprite = beastSprite;
            abilityText.text = "Beast";
        }

        if(!isGhost && !isBeast && !possessedEnemy)
        {
            abilityImage.sprite = normalSprite;
            abilityText.text = "Human";
        }
        
    }

    //Check if we've possessed an enemy
    public void PossessEnemy()
    {
        if (possessedEnemy)
        {
            isGhost = false;
            anim.SetBool("isPossessing", true);
            Debug.Log("Possessed Enemy In Player Abilities");
            abilityImage.sprite = possessedSprite;
            abilityText.text = "Possessed";
            cooldownImage.gameObject.SetActive(true);
        }
        else
        {
            anim.SetBool("isPossessing", false);
            StopCoroutine(SpeedEnemy());
        }

        if (isCooldown)
        {
            cooldownImage.fillAmount -= 1/cooldown * Time.deltaTime;
        }
        else
        {
            cooldownImage.fillAmount = 1;
            cooldownImage.gameObject.SetActive(false);
        }
        

    }

    //Just an example
    private void EnemySpeedAbility()
    {
        if (speedAbility)
        {
            
            playerController.sprintSpeed = 20;
            playerController.walkSpeed = 15;
            anim.SetBool("isPossessing", true);
            //StartCoroutine(SpeedEnemy());
        }
        else
        {
            playerController.sprintSpeed = savedSprintSpeed;
            playerController.walkSpeed = savedWalkSpeed;
        }
    }

    //Just stopping it on a timer for now
    public IEnumerator SpeedEnemy()
    {
        //Teleports player inside the enemy, makes enemy a child of player so controller functions, makes enemy face the players rotation, disables players body
        yield return new WaitForSeconds(5);
        isCooldown = true;
        this.gameObject.transform.position = possessableEnemy.transform.position;
        possessableEnemy.GetComponent<CapsuleCollider>().isTrigger = true;

        possessableEnemy.transform.parent = this.gameObject.transform;
        possessableEnemy.transform.rotation = this.gameObject.transform.rotation;

        playerRigJoints.SetActive(false);
        playerRigSurface.SetActive(false);

        //Removes enemy as child from any parent, sets body active again, turns off bools
        yield return new WaitForSeconds(cooldown);

        speedAbility = false;
        possessedEnemy = false;
        isCooldown = false;
        isGhost = true;
        possessableEnemy.transform.parent = null;

        playerRigJoints.SetActive(true);
        playerRigSurface.SetActive(true);
        possessableEnemy.GetComponent<CapsuleCollider>().isTrigger = false;
        gameObject.layer = 12;
        yield return null;

    }

    public IEnumerator TurnBeast()
    {
        yield return new WaitForSeconds(2);
        beastRig.SetActive(true);
        playerRig.SetActive(false);
        anim.SetBool("turnBeast", false);

    }

    //What happens when the spirit should return to the body
    public IEnumerator ResurrectionBehavior()
    {
        //Camera fades to black and audio plays
        //anim.applyRootMotion = false;
        cameraEffects.FadeIn();
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(spiritDeath);

        //Waits until after camera completely fades and then destroys instantiated body
        yield return new WaitForSeconds(4);

        Destroy(droppedBody);
        playerWeapon.SetActive(true);

        //Fills variable with the empty game object instantiated OnDestroy from the body
        yield return new WaitForSeconds(.5f);

        transformPosition = GameObject.FindGameObjectWithTag("TransformPosition");

        //Teleports player to the transform we just set above and sets ghost state to false
        yield return new WaitForSeconds(.5f);

        this.gameObject.transform.position = transformPosition.transform.position;
        isGhost = false;

        //Fades the camera back in, destroys teleport transform, makes sure bools are set to false, allows companions to follow again
        yield return new WaitForSeconds(2);

        cameraEffects.FadeOut();
        Destroy(transformPosition);
        anim.SetBool("resurrect", false);

        /*if (hasMeleeCompanion)
        {
            meleeMovement.wait = false;
            meleeMovement.follow = true;
        }

        if (hasHealerCompanion)
        {
            healerMovement.wait = false;
            healerMovement.follow = true;
        }*/

    }

    //What happens when the player should turn into a ghost
    public IEnumerator DeathBehavior()
    {
        //Plays audio on death
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
        AudioManager.instance.PlayClip(spiritForm);
        

        //Instantiates a body for the illusion of the player popping up as a spirit, sets droppedBody variable
        yield return new WaitForSeconds(3f);
        Instantiate(bodyPrefab, bodySpawnPosition.transform.position, bodySpawnPosition.transform.rotation);
        droppedBody = GameObject.FindGameObjectWithTag("DroppedBody");
        playerWeapon.SetActive(false);
        
    }
}
