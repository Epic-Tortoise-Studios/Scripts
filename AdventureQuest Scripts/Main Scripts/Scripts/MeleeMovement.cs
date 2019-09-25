using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MeleeMovement : MonoBehaviour
{
    //This needs an Enemy with the Tag "Enemy" and a Layer "Enemy" to work

    public bool isActive = false;

    public Transform player;
    private GameObject playerObject;
    private Transform enemy;
    private GameObject enemyObject;
    private Rigidbody rb;
    private NavMeshAgent navAgent;
    private Collider[] inAggroRadius; //Stores the enemies that the pluse sphere finds
    private float isDamaged;//Checking for it the player is damaged in Defender State

    [Header("Player/Enemy Tracking")]
    public LayerMask enemyLayer; //Layer on GameObjects the pulse sphere is looking for
    public float speed;//How fast the Companion moves
    public float playerTrackingDistance;//Distance it looks for Player in the scene
    public float enemyTrackingDistance;//Distance it will stay locked to the enemy
    public float distance;//Distance Companion keeps from enemy & player
    public float aggroRadius;//Radius the pulse sphere checking for enemies will go
    public float respawnTimer;//Time for Companion to respawn after "sacrifice"

    [Header("Current State")]  //For testing purposes but "Header" can be removed and bools can be made private in final
    public bool aggressive = false;
    public bool passive = false;
    public bool defender = false;
    public bool wait = true;
    public bool follow = false;
    public bool sacrifice = false;
    
    [Header("Attack")]
    public GameObject wepTrigger;//The Trigger/Damage Volume on the weapon
    private bool attkRdy = true;
    public float attkSpeed;//Speed the Companion attacks (How offten the attk animation plays)
    static Animator anim;


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody>();
        navAgent = GetComponent<NavMeshAgent>();
        isDamaged = playerObject.GetComponent<PlayerHealth>().Health;
    }

    void FixedUpdate()
    {
        if (aggressive && isActive == true)
        {
            //First line below searches for any object within float "aggroRadius" that has the specified Layer...
            //On update will pulse sphere around the companion and check all objects layers within it.
            inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));
            rb.constraints = RigidbodyConstraints.None;
            if (inAggroRadius.Length > 0) //Checks array to see if the sphere found the specified layer on pulse
            {
                //follow = false;
                //These next 2 lines will update "enemy", so after one dies it will fill "enemy" with next enemy in array if multiple
                enemyObject = GameObject.FindGameObjectWithTag("Enemy"); 
                enemy = enemyObject.transform;
                //Below is classic find thing move to it script we've been using
                if (Vector3.Distance(enemy.position, this.transform.position) < enemyTrackingDistance)
                {
                    this.transform.position = (transform.position - enemy.transform.position).normalized * distance + enemy.transform.position;
                    follow = false;
                    transform.LookAt(enemy);
                    Vector3 direction = enemy.transform.position - this.transform.position;
                    transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    StartCoroutine(AttackAnim());
                }
                if (Vector3.Distance(enemy.position, this.transform.position) <= distance)//Checks distance from target
                {
                    //Keeps "distance" from target
                    this.transform.position = (transform.position - enemy.transform.position).normalized * distance + enemy.transform.position;
                    anim.SetBool("isIdle", true);
                    anim.SetBool("isMove", false);
                }
            }
            else if (inAggroRadius.Length == 0)   //If it doesn't find an object with the layer it executes FollowPlayer
            {
                FollowPlayer();
            }
        }

        //All of these are just checking which State the Companion is in and executing accordingly
        else if (passive == true)
        {
            FollowPlayer();
        }

        else if (defender == true)
        {
            DefendPlayer();
        }

        else if (sacrifice && wait == true)
        {
            StartCoroutine(Respawn());
        }

        else if ( wait == true)
        {
            aggressive = false;
            defender = false;
            follow = false;
            passive = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            anim.SetBool("isIdle", true);
            anim.SetBool("isMove", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isDead", false);
        }

        else if(follow == true)
        {
            wait = false;
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
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }

    public void FollowPlayer()
    { 
        //Same basic tracking script we've been using forever
        if (Vector3.Distance(player.position, this.transform.position) < playerTrackingDistance)
        {
            anim.SetBool("isDead", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isMove", true);
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) <= distance) 
        {
            this.transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;
            anim.SetBool("isIdle", true);
            anim.SetBool("isMove", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isDead", false);
        }
    }

    public void DefendPlayer()
    {
        if (PlayerHealth.Instance.Health < 4 )
        {
            aggressive = true;
            defender = false;
            follow = true;
            wait = false;
        }
    }

    public void Sacrifice()
    {
            PlayerHealth.Instance.Heal(2f);
            anim.SetBool("isMove", false);
            anim.SetBool("isAttack", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isDead", true);
    }

    //Below is for looping the Attack Animation when Enemy is in range
    public IEnumerator AttackAnim()
    {
        if(attkRdy == true)
        {
            wepTrigger.SetActive(true);
            anim.SetBool("isMove", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isAttack", true);
            attkRdy = false;
            yield return new WaitForSeconds(attkSpeed);
            attkRdy = true;
            
        }
    }

    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        follow = true;
        sacrifice = false;
        wait = false;
    }
}
