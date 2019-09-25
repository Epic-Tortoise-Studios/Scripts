using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompMoveTest : MonoBehaviour
{
    [Header("Current State")]
    public bool isActive;
    public bool aggressive = false;
    public bool passive = false;
    public bool wait = true;

    [Header("Weapon")]
    public GameObject wepTrigger;
    public GameObject wepSpecialTrigger;
    public GameObject specialEffect;

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
    public float respawnTimer;
    private Collider bodyCollider;

    [Header("Private Bool's")]
    bool basicAttack;
    bool specialAttack;
    bool canAttack;
    bool canHeal;
    
    void Start()
    {
        bodyCollider = GetComponent<Collider>();

        canAttack = true;
        canHeal = true;
        
        basicAttack = false;
        specialAttack = false;
        
        wepTrigger.SetActive(false);
        wepSpecialTrigger.SetActive(false);
    }

    void Update()
    {

        if (specialAttack)
        {
            bodyCollider.isTrigger = false;
            anim.SetTrigger("special1");
            wepSpecialTrigger.SetActive(true);
            anim.SetBool("isMove", false);
            anim.SetBool("isIdle", false);
            specialAttack = false;
        }
        if (basicAttack)
        {
            bodyCollider.isTrigger = false;
            anim.SetTrigger("basicAttk");
            anim.SetBool("isMove", false);
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
                if (Vector3.Distance(target.transform.position, gameObject.transform.position) >= 2)
                {
                    anim.SetBool("isMove", true);
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
                else if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 3 && canAttack)
                {
                    int rand = Random.Range(0, 4);

                    if (rand == 0)
                    {
                        canAttack = false;
                        StartCoroutine(Special1());
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

            if(inAggroRadius.Length == 0)
            {
                FollowPlayer();
            }
        }
            
        if (passive)
        {
            FollowPlayer();
        }

        if(wait)
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
            anim.SetBool("isMove", true);
            anim.SetBool("isIdle", false);
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) <= distance)
        {
            this.transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;
            anim.SetBool("isIdle", true);
            anim.SetBool("isMove", false);
        }
    }

    IEnumerator Attack2() //When it's at the enemy
    {
        basicAttack = true;
        yield return new WaitForSeconds(.5f);
        wepTrigger.SetActive(true);
        yield return new WaitForSeconds(.5f);
        wepTrigger.SetActive(false);
        yield return new WaitForSeconds(1f);
        anim.SetBool("isIdle", true);
        anim.ResetTrigger("basicAttk");
        anim.SetBool("isMove", false);

        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    IEnumerator Special1()
    {
        specialAttack = true;
        specialEffect.SetActive(true);

        yield return new WaitForSeconds(4f);
        anim.SetBool("isIdle", true);
        anim.ResetTrigger("special1");
        specialEffect.SetActive(false);
        wepSpecialTrigger.SetActive(false);
        anim.SetBool("isMove", false);

        yield return new WaitForSeconds(2f);
        canAttack = true;
    }

    public void Sacrifice()
    {
        if (canHeal)
        {
            canHeal = false;
            wait = true;

            PlayerHealth.Instance.Heal(2f);
            anim.SetBool("isMove", false);
            anim.SetBool("isIdle", false);
            anim.SetBool("isHealing", true);
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        anim.SetBool("isIdle", true);
        anim.SetBool("isHealing", false);
        wait = false;
        passive = true;
        canHeal = true;
    }
}
