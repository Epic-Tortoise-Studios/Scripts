using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    public Transform[] patrolPoints;
    private int destPoint = 0;
    public GameObject player;
    public Transform playerTransform;
    public NavMeshAgent enemy;
    public float Abs;
    public int chaseDistance;
    public GameObject lastPosition;

    public GameObject enemyHealth;
    public EnemyHealth eHealth;

    public Animator enemyanim;
    //public Transform goal;

    public int range = 10;
    public int range2 = 1;
    public float currentSpeed;
    public Vector3 lastvelocity;
    public Vector3 agentvelocity;
    public float patrolTime = 3f;

    public Rigidbody enemyrb;

    public float patrolSwitch = 3;

    public AudioSource ScreamSound;

    public bool welcome;
    public float attackDistance;

    public bool InCombat;

    public float patrolWaitTime;
    public bool Patrolling;

    public bool AttackingNow;

    public bool AttackAnimation;

    public bool MovingtoPatrol;

    public bool Pursuing;
    public bool Running;
    public bool Wicked;

    public bool FacingPlayer;
    public float currentAttackSpeed;

    public float EnemyDamageTimeout = 5;
    public EnemyDamagePlayer EnemyDamagePlayer;

    int once = 0;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.GetComponent<Transform>();
        enemy = GetComponent<NavMeshAgent>();

        chaseDistance = 10;
        enemyanim = GetComponentInChildren<Animator>();
        currentSpeed = enemy.speed;
        patrolSwitch = 3;
        //Invoke("Patrol", 0);
        Wicked = false;

        welcome = false;
        enemyrb = GetComponent<Rigidbody>();
        once = 0;
        attackDistance = 1f;
        Patrolling = false;
        InCombat = false;
        patrolWaitTime = 3f * Time.deltaTime;
        enemyanim.SetBool("IsIdle", true);
        print("idle on");
        AttackingNow = false;
        MovingtoPatrol = false;
        Running = false;
        currentAttackSpeed = enemyanim.GetFloat("AttackSpeed");
        EnemyDamagePlayer = GetComponentInChildren<EnemyDamagePlayer>();
        EnemyDamageTimeout = EnemyDamagePlayer.damageTimeout;
    }

    void Update()
    {
        currentAttackSpeed = enemyanim.GetFloat("AttackSpeed");
        //Player Dead
        if (PlayerStats.Instance.Health <= 0 || (enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Dying") && enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f))
        {
            once = 4;
            Debug.Log("Player is dead");
            PlayerKilled();
        }

        else if (enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Zombie Dying") && enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            once = 4;
            Debug.Log("Enemy is Dead");

        }

        else if(Wicked == true)
        {

        }

        //Greater than the ChaseDistance, In Combat & Not Patrolling
        //This is exiting Combat Script
        else if (((Mathf.Abs(Vector3.Distance(enemy.transform.position,
        player.transform.position)) > chaseDistance)) && InCombat == true && Patrolling == false && once < 4)
        {
            InCombat = false;
            Invoke("Patrol", 0f);
            Invoke("ArrivedatPatrol", 1f);
            print("Out of Combat");
        }

        //Beginning of Welcome
        else if ((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) <= chaseDistance) && once == 0 && InCombat == true)
        {
            Invoke("WelcomeLook", 0f);
            Invoke("Welcome", 3f);
        }

        else if ((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) <= chaseDistance) && once == 1 && InCombat == true)
        {
            Invoke("EndWelcome", 0f);
        }

        //Enemy within attack distance and not attacking at the moment
        else if ((welcome == true) && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) <= chaseDistance)
        && once == 2 && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) < attackDistance)
        && InCombat == true && AttackingNow == false && FacingPlayer == true)
        {
            Invoke("FacingPlayer", 0f);
        }


        else if ((welcome == true) && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) <= chaseDistance)
        && once == 2 && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) < attackDistance)
        && InCombat == true && AttackingNow == false  && currentAttackSpeed == 1.0f)
        {
            Invoke("FaceTarget", 0f);
            print("Attacking");
            Invoke("AttackPlayer", .5f);
            once = 2;
            print("AttackingNow turned off, can pursue");
        }

        else if ((welcome == true) && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) <= chaseDistance)
        && once == 2 && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) < attackDistance)
        && InCombat == true && AttackingNow == false && currentAttackSpeed == 2.0f)
        {
            Invoke("FaceTarget", 0f);
            print("Attacking");
            Invoke("AttackPlayerFast", .5f);
            once = 2;
            print("AttackingNow turned off, can pursue");
        }
        /*//Enemy is within attack distance and in the Running Animation
        else if ((welcome == true) && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position))
            <= attackDistance) && once == 2 && InCombat == true && AttackingNow == false 
                && enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Running") && enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                print("Attacking");
                Invoke("AttackPlayer", 0f);
                Invoke("Attacking", 4);
                once = 2;
            }*/

        //Enemy in chasing distance, but outside of attack distance = Pursuing Player
        else if ((welcome == true) && (Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) <= chaseDistance)
                && once == 2 && Mathf.Abs(Vector3.Distance(enemy.transform.position, player.transform.position)) > attackDistance
                && InCombat == true && AttackingNow == false  && Pursuing == false)
            {
                print("Pursuing");
                Invoke("Pursue", 0f);
            }


            //when reached the stopping point
            else if (((Mathf.Abs(Vector3.Distance(enemy.transform.position,
                player.transform.position)) > chaseDistance)) && InCombat == false && Patrolling == false && MovingtoPatrol == true && once < 4)
            {
                print("Stopping at Patrol");
                Invoke("Stop", 0f);
                Invoke("StopPatrol", 0f);
            }

            //Outside of ChaseDistance, Exited combat, Is Patrolling so started Stop or Patrol and is currently in Patrol
            else if (((Mathf.Abs(Vector3.Distance(enemy.transform.position,
                player.transform.position)) > chaseDistance)) && InCombat == false && Patrolling == true && MovingtoPatrol == false && once < 4)
            {
                print("Moving to Next Point");
                Invoke("Stop", 0f);
                Invoke("StopPatrol", 0);
                Invoke("Patrol", 0f);
                Invoke("ArrivedatPatrol", 1f);
            }

            //Outside of chaseDistance, not in combat, in Idle State at Patrol Point, isn't moving
            //Starts moving player to Patrol
            else if (((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) > chaseDistance)) && InCombat == false && Patrolling == false && MovingtoPatrol == false && once < 4)
            {
                print("Moving to Next Point");
                Invoke("Patrol", 0f);
                Invoke("ArrivedatPatrol", 1f);
            }

            //Outside of Chase Distance, has not started Patrolling.
            else if (((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) > chaseDistance)) && InCombat == false && Patrolling == false && MovingtoPatrol == false && once < 4)
            {
                print("Starting Up");
                Invoke("Patrol", 0f);
                Invoke("ArrivedatPatrol", 1f);
            }

            //This is navigation and needs to continue being controlled here
            //So extending distance comparison to Enemy to Nav Point
            else if ((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) > chaseDistance)
            && (!enemy.pathPending && enemy.remainingDistance < 1f)
            && InCombat == false && Patrolling == true && MovingtoPatrol == true && once < 4)
            {
                print("Stopping at Patrol");
                Invoke("Stop", 0f);
                Invoke("StopPatrol", 0);
                Invoke("Patrol", 0);
            }
            //Enemy inside Chasing Distance:  Turns Combat on and Patrolling Off
            else if ((Mathf.Abs(Vector3.Distance(enemy.transform.position,
            player.transform.position)) <= chaseDistance) && once < 4)
            {
                InCombat = true;
                Patrolling = false;

            }
        enemyanim.SetFloat("RunningSpeed", Vector3.Magnitude(enemy.velocity));
           
    }

    void Pursue()
    {
        Pursuing = true;
        FaceTarget();
        Patrolling = false;
        InCombat = true;
        enemy.isStopped = false;
        enemy.destination = player.transform.position;

        print("Running in Pursue");
        Invoke("DonePursuing", 0f);
    }

    void DonePursuing()
    {
        Pursuing = false;
        print("Stop Pursuing");
    }

    void AttackPlayer()
    {
        AttackingNow = true;
        print("Attacking now is on, cannot pursue");
        FaceTarget();
        Patrolling = false;
        //once = 3;
        InCombat = true;
        enemy.isStopped = true;
        enemyanim.SetBool("IsRunning", false);
        print("Turned off Running");
        Running = false;
        enemyanim.SetTrigger("AttackPlayer");
        print("Starting Attacking");
        enemyanim.SetBool("IsIdle", true);
        Invoke("Attacking", 4);

    }

    void AttackPlayerFast()
    {
        AttackingNow = true;
        print("Attacking now is on, cannot pursue");
        FaceTarget();
        Patrolling = false;
        //once = 3;
        InCombat = true;
        enemy.isStopped = true;
        enemyanim.SetBool("IsRunning", false);
        print("Turned off Running");
        Running = false;
        enemyanim.SetTrigger("AttackPlayer");
        print("Starting Attacking");
        enemyanim.SetBool("IsIdle", true);
        Invoke("Attacking", 2);
        print("AttackFast");
        EnemyDamageTimeout = EnemyDamagePlayer.damageTimeout = 2;
    }

    void Attacking()
    {
        Running = false;
        Pursuing = false;
        AttackingNow = false;
        print("AttackingNow false");
        //transform.Translate(Vector3.forward * Time.deltaTime);
     }

    void StopAttacking()
    {
        CancelInvoke();
    }
    void Stop()
    {
        //enemyanim.SetBool("IsIdle", false);
        MovingtoPatrol = false;
        InCombat = false;
        Patrolling = true;
        enemy.isStopped = true;
        //enemyanim.SetBool("IsRunning", false);
        Running = false;
        enemyanim.SetBool("IsIdle", true);
        print("idle on");
    }
    void StopPatrol()
    {
        //enemyanim.SetBool("IsRunning", false);
        enemyanim.SetBool("IsIdle", true);
        print("Idling");
    }

    void Patrol()
    {
            Pursuing = false;
            MovingtoPatrol = true;
            Patrolling = true;
            enemy.isStopped = false;
            InCombat = false;

        if (patrolPoints.Length == 0)
        {
            //enemyanim.SetBool("IsRunning", false);
            Running = false;
            enemyanim.SetBool("IsIdle", true);
            print("No Patrol Points");
        }

        else if (patrolPoints.Length > 0)
        {
            //enemyanim.SetBool("IsRunning", true);
            print("Running on");
            enemy.destination = patrolPoints[destPoint].position;
            destPoint = (destPoint + 1) % patrolPoints.Length;
        }

    }

    void ArrivedatPatrol()
    {

    }

     void Idle()
    {
        Running = false;
        Pursuing = false;
        Patrolling = true;
        enemyanim.SetBool("IsIdle", true);
        print("Idle Function");
    }

    void FaceTarget()
    {
        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private IEnumerator PatrolTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(patrolTime);

            Debug.Log("TimerUp");
        }

    }

    void EnemyDead()
    {
        once = 4;
        enemy.isStopped = true;
        InCombat = false;
    }

    void PlayerKilled()
    {
        once = 4;
        InCombat = false;
        enemy.isStopped = true;
        enemyanim.SetBool("PlayerDead", true);
    }

    void WelcomeLook()
    {
        FaceTarget();
        enemyanim.SetBool("IsIdle", false);
        enemyanim.SetBool("IsRunning", false);
        Running = false;
        enemyanim.SetTrigger("SawPlayer");
        ScreamSound.Play(0);
        InCombat = true;
        enemy.isStopped = true;
        once = 1;
    }

    void Welcome()
    {
        once = 1;
        Debug.Log("SawPlayer");
        ScreamSound.Play(0);
        //enemy.transform.LookAt(playerTransform);              
        enemy.isStopped = true;
        FaceTarget();
        welcome = true;
    }

    void EndWelcome()
    {
        enemyanim.SetBool("IsRunning", true);
        Running = true;
        once = 2;
        FaceTarget();
        InCombat = true;
        enemy.isStopped = false;
        Debug.Log("unFroze Player");
    }
}
