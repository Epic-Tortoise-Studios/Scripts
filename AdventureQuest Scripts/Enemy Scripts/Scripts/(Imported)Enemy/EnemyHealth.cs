using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{

    public float health;
    public float maxHealth;
    public float deathTimer = 3f;
    public float HurtTimer = 1f;

    public Canvas healthBarUI;
    public Slider slider;

    public Animator hurt;
    public Transform StopMoving;

    public Rigidbody rb;

    public GameObject removeDamage;

    public AudioSource hurtSound;
    public AudioSource dyingSound;
    public AudioSource ScreamSound;

    public Animator WeaponAnimation;

    public GameObject HandVFX;
    public GameObject BodyVFX;

    public Animation attackAnim;

    public int WickedDamageThresholdAmount = 50;

    public float AttackSpeedMultiplier;

    public bool isDead;
    public PlayerStats playerStats;
    //public PlayerStats baseXP;

    //public EnemyDefensivePatrol waitTime;
    //public Animator dying;

    void Start()
    {
        isDead = false;
        Debug.Log("Enemy lives");
        hurtSound = GetComponent<AudioSource>();

        health = maxHealth;
        slider.value = CalculateHealth();
        StopMoving = GetComponent<Transform>();
        healthBarUI = GetComponentInChildren<Canvas>();
        slider = GetComponentInChildren<Slider>();
        rb = GetComponentInChildren<Rigidbody>();
        healthBarUI = GetComponentInChildren<Canvas>();
        hurt = GetComponentInChildren<Animator>();

        //HandVFX = GameObject.FindGameObjectWithTag("EnemyHandSlowVFX");
        HandVFX.SetActive(false);

        //BodyVFX = GameObject.FindGameObjectWithTag("EnemyBodySlowVFX");
        BodyVFX.SetActive(false);

        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        //baseXP = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = CalculateHealth();

        if (health <= maxHealth)
        {
            healthBarUI.gameObject.SetActive(true);
        }

        if (health <= 0)
        {
            slider.gameObject.SetActive(false);
            gameObject.GetComponent<NavMeshAgent>().isStopped = true;
            Dying();
            Death();
        }

        if (health > maxHealth)
        {
            health = maxHealth;
        }

        if(health <= WickedDamageThresholdAmount)
        {
            HandVFX.SetActive(true);
            BodyVFX.SetActive(true);
            hurt.SetFloat("AttackSpeed", 2.0f);
            //hurt.SetTrigger("JumpAttackTime");
        }
        
    }

    float CalculateHealth()
    {
        return health / maxHealth;
    }

    public void HurtEnemy(int damageToGive)
    {
        if (health > 0)
        {
            health -= damageToGive;
            hurt = gameObject.GetComponentInChildren<Animator>();
            hurt.SetTrigger("Hurt");
            hurtSound.Play(0);
        }
    }

    public IEnumerator Timer()
    {

        yield return new WaitForSeconds(deathTimer);
        

    }

    public void Wicked()
    {
        
    }


    public void Dying()
    {
        //rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;
        hurt.SetBool("Dead", true);
        removeDamage.SetActive(false);
        //WeaponAnimation.SetBool("Dead", true);
        //StartCoroutine(Timer());
        //waitTime.GetComponent<EnemyDefensivePatrol>().Patrol();
        //Creates a Cool Ghost Effect
        //waitTime.waitTime = 3;
        
    }

    public void Death()
    {
        //WeaponAnimation.SetBool("Dead", true);
        //Destroy(transform.parent.gameObject,3);
        isDead = true;
        Debug.Log("Enemy Died");
        Destroy(gameObject, 5);
        HandVFX.SetActive(false);
        BodyVFX.SetActive(false);
    }

    public void GiveXP()
    {
        if (isDead == true)
        {
            playerStats.GainXP(playerStats.baseXP);
            Debug.Log("You gain xp");
        }
    }

    public void OnDestroy()
    {
        GiveXP();
    }
}
