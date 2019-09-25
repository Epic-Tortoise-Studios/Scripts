using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HealerMovement : MonoBehaviour
{
    //This needs an Enemy with the Tag "Enemy" and a Layer "Enemy" to work
    private Transform player;
    private GameObject playerObject;
    private Transform enemy;
    private GameObject enemyObject;
    private Rigidbody rb;
    private NavMeshAgent navAgent;
    static Animator anim;
    private Collider[] inAggroRadius;

    public PlayerHealth playersHealth;
    public GameObject healAura;

    [Header("Current State")]  //For testing purposes but "Header" can be removed and bools can be made private in final
    public bool isActive;
    public bool aggressive;
    public bool passive;
    public bool follow;
    public bool wait = true;
    public bool healing = true;

    [Header("Floats")]
    public float speed;
    //public float sprint;
    public float playerTrackingDistance;
    public float playerMaxRange;
    public float keepDistance;
    public float enemyDistance;
    public float healDelay;

    [Header("Enemy Tracking")]
    public LayerMask enemyLayer; //Layer on GameObjects the pulse sphere is looking for
    public float enemyTrackingDistance;//Distance it will stay locked to the enemy
    public float aggroRadius;//Radius the pulse sphere checking for enemies will go

    [Header("Attack")]
    public GameObject wepTrigger;//The Trigger/Damage Volume on the weapon
    private bool attkRdy = true;
    public float attkSpeed;//Speed the Companion attacks (How offten the attk animation plays)


    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        anim = GetComponent<Animator>();
    }



    void FixedUpdate()
    {
        if (aggressive && isActive == true)
        {
            //First line below searches for any object within float "aggroRadius" that has the specified Layer...
            //On update will pulse sphere around the companion and check all objects layers within it.
            inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));

            if (inAggroRadius.Length > 0) //Checks array to see if the sphere found the specified layer on pulse
            {
                follow = false;
                //These next 2 lines will update "enemy", so after one dies it will fill "enemy" with next enemy in array if multiple
                enemyObject = GameObject.FindGameObjectWithTag("Enemy");
                enemy = enemyObject.transform;
                //Below is classic find thing move to it script we've been using
                if (Vector3.Distance(enemy.position, this.transform.position) < enemyTrackingDistance)
                {
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    transform.LookAt(enemy);
                    Vector3 direction = enemy.transform.position - this.transform.position;
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    StartCoroutine(AttackAnim());
                }
                if (Vector3.Distance(enemy.position, this.transform.position) <= keepDistance)//Checks distance from target
                {
                    //Keeps "distance" from target
                    this.transform.position = (transform.position - enemy.transform.position).normalized * enemyDistance + enemy.transform.position;
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isWalk", false);
                }
            }
            else if (inAggroRadius.Length == 0)   //If it doesn't find an object with the layer it executes FollowPlayer
            {
                FollowPlayer();
            }
        }

        else if (wait == true)
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", true);
            follow = false;
            passive = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (healing == true && PlayerHealth.Instance.Health < 2)
        {
            StartCoroutine(HealPlayer());
        }

        else if(PlayerHealth.Instance.Health >= 2)
        {
            if (follow == true)
            {
                FollowPlayer();
            }
        }

        //All of these are just checking which State the Companion is in and executing accordingly
        else if (passive == true)
        {
            FollowPlayer();
        }
        
        else if (follow == true)
        {
            wait = false;
            rb.constraints = RigidbodyConstraints.None;
            FollowPlayer();
        }

        else if (isActive == false)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

        else if (wait == false)
        {
            if (isActive == true)
            {
                follow = false;
                passive = false;
            }
        }
    }

    void FollowPlayer()
    {
        rb.constraints = RigidbodyConstraints.None;
        wait = false;
        anim.SetBool("isWalk", true);
        anim.SetBool("isAttack", false);
        //Same basic tracking script we've been using forever
        /*if (Vector3.Distance(player.position, this.transform.position) > playerMaxRange)
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isWalk", false);
            anim.SetBool("isRun", true);
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * sprint);
        }*/
        if (Vector3.Distance(player.position, this.transform.position) <= playerTrackingDistance)
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", false);
            //anim.SetBool("isRun", false);
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) < keepDistance)
        {
            this.transform.position = (transform.position - player.transform.position).normalized * keepDistance + player.transform.position;
            anim.SetBool("isDead", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", true);
        }
    }

    public IEnumerator AttackAnim()
    {
        if (attkRdy == true)
        {
            //wepAnim.SetTrigger("Active");
            wepTrigger.SetActive(true);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", true);
            attkRdy = false;
            yield return new WaitForSeconds(attkSpeed);
            anim.SetBool("isAttack", false);
            attkRdy = true;

        }
    }

    public IEnumerator HealPlayer()
    {
        if (healing == true)
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isWalk", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isHealing", true);
            rb.constraints = RigidbodyConstraints.FreezeAll;
            Instantiate(healAura, transform.position + (transform.forward * 2), transform.rotation);
            healing = false;
            anim.SetBool("isHealing", false);
            yield return new WaitForSeconds(healDelay);
            healing = true;
        }
        
    }
}
