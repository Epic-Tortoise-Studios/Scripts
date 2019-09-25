using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroMovement : MonoBehaviour
{
    [Header("Current State")]
    public bool isActive;
    public bool aggressive = false;
    public bool passive = false;
    public bool wait = true;

    [Header("Weapon")]
    public GameObject minion;
    public GameObject spellCircle;

    [Header("Player")]
    public Transform player;
    public float playerTrackingDistance;
    public float distance;

    [Header("Enemy")]
    public LayerMask enemyLayer;
    public float aggroRadius;
    private Collider[] inAggroRadius;
    Collider target;

    [Header("Companion Variables")]
    public Animator anim;
    public float speed;
    private Collider bodyCollider;
    
    [Header("Private Bool's")]
    bool basicAttack;
    //bool specialAttack;
    bool canAttack;


    void Start()
    {
        bodyCollider = GetComponent<Collider>();

        canAttack = true;

        basicAttack = false;
        //specialAttack = false;
    }

    void Update()
    {
        /*if (specialAttack)
        {
            bodyCollider.isTrigger = false;
            anim.SetTrigger("special1");
            anim.SetBool("isMove", false);
            anim.SetBool("isIdle", false);
            specialAttack = false;
        }*/
        if (basicAttack)
        {
            bodyCollider.isTrigger = false;
            anim.SetTrigger("isAttack");
            anim.SetBool("isWalking", false);
            anim.SetBool("isIdle", false);
            basicAttack = false;
        }
    }

    void FixedUpdate()
    {
        if (aggressive)
        {
            inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));
            target = inAggroRadius[0];

            if (inAggroRadius.Length > 0)
            {
                if (Vector3.Distance(target.transform.position, gameObject.transform.position) > 6)
                {
                    anim.SetBool("isWalking", true);
                    anim.SetBool("isIdle", false);
                    Vector3 targetPosition = target.transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);

                    float distance = Vector3.Distance(target.transform.position, this.transform.position);
                    if (distance >= 1)
                    {
                        transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    }
                }
                if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 7 && canAttack)
                {
                    int rand = Random.Range(0, 4);

                    if (rand == 0)
                    {
                        canAttack = false;
                        StartCoroutine(Attack2());
                    }
                    if (rand == 1)
                    {
                        canAttack = false;
                        StartCoroutine(Attack2());
                    }
                    if (rand == 2)
                    {
                        canAttack = false;
                        StartCoroutine(Attack2());
                    }
                    if (rand == 3)
                    {
                        canAttack = false;
                        StartCoroutine(Attack2());
                    }
                }
            }

            if (inAggroRadius.Length == 0)
            {
                FollowPlayer();
            }
        }

        if (passive)
        {
            FollowPlayer();
        }

        if (wait)
        {
            passive = false;
            aggressive = false;
        }
    }

    public void FollowPlayer()
    {
        //Same basic tracking script we've been using forever
        if (Vector3.Distance(player.position, this.transform.position) < playerTrackingDistance)
        {
            anim.SetBool("isWalking", true);
            anim.SetBool("isIdle", false);
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) <= distance)
        {
            this.transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
        }
    }

    IEnumerator Attack2() //When it's at the enemy
    {
        basicAttack = true;
        Instantiate(spellCircle, transform.position + (transform.forward * 2), transform.rotation * Quaternion.Euler(-90, 0, 0));
        yield return new WaitForSeconds(1.8f);
        Instantiate(minion, transform.position + (transform.forward * 2), transform.rotation);
        
        yield return new WaitForSeconds(2f);
        anim.SetBool("isIdle", true);
        anim.ResetTrigger("isAttack");
        anim.SetBool("isWalking", false);

        yield return new WaitForSeconds(2f);//attack speed
        canAttack = true;
    }

    /*IEnumerator Special1()
    {
        specialAttack = true;
        yield return new WaitForSeconds(1.5f);
        Instantiate(specialAttackPrefab, transform.position + (transform.forward * 6), transform.rotation * Quaternion.Euler(90, 90, 0));

        yield return new WaitForSeconds(2f);
        anim.ResetTrigger("special1");
        anim.SetBool("isIdle", true);
        anim.SetBool("isMove", false);

        yield return new WaitForSeconds(2f);//attack speed
        canAttack = true;
    }*/
}
