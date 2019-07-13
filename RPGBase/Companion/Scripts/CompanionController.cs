using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionController : MonoBehaviour
{
    [Header("Current State")]
    public bool isActive;
    public bool aggressive = false;
    public bool passive = false;
    public bool wait = true;

    [Header("Attack")]
    public GameObject RangedSpellPrefab;
    public float autoAttackCooldown;
    public float autoAttackCurTime;
    public bool canAutoAttack;

    [Header("Player")]
    public Transform player;
    public GameObject playerBody;
    private UserStats userStats;
    public float playerTrackingDistance;
    public float distance;

    [Header("ObjectFind")]
    public GameObject Target;
    public float objectRadius;

    [Header("Enemy")]
    public LayerMask enemyLayer;
    public float aggroRadius;
    private Collider[] inAggroRadius;
    Collider target;

    [Header("Companion Variables")]
    public float speed;
    private Collider bodyCollider;
    public float respawnTimer;

    [Header("Private Bool's")]
    bool basicAttack;
    bool specialAttack;
    bool canAttack;
    bool canHeal;

    void Start()
    {
        playerBody = GameObject.FindGameObjectWithTag("Player");
        userStats = playerBody.GetComponent<UserStats>();

        canAttack = true;
        canHeal = true;

        basicAttack = false;
        specialAttack = false;
    }

    void Update()
    {
        if (aggressive)
        {
            if (userStats.selectedUnit != null)
            {
                float distance = Vector3.Distance(this.transform.position, userStats.selectedUnit.transform.position);
                Vector3 targetDir = userStats.selectedUnit.transform.position - transform.position;
                Vector3 forward = transform.forward;
                float angle = Vector3.Angle(targetDir, forward);

                if (angle > 60.0)
                {
                    canAttack = false;
                    autoAttackCurTime = 0;
                }
                else
                {
                    if (distance < 60)
                    {
                        canAttack = true;
                    }
                    else
                    {
                        canAttack = false;
                        autoAttackCurTime = 0;
                    }
                }

                if (userStats.selectedUnit != null && canAttack && canAutoAttack == true)
                {
                    if (autoAttackCurTime < autoAttackCooldown)
                    {
                        autoAttackCurTime += Time.deltaTime;
                    }
                    else
                    {
                        Attack();
                        autoAttackCurTime = 0;
                    }
                }
            }
            if (userStats.selectedUnit = null)
            {
                FollowPlayer();
            }
        }
        if (passive)
        {
            Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            Collider[] hitColliders = Physics.OverlapSphere(center, objectRadius);
            int i = 0;
            while (i < hitColliders.Length)
            {
                if (hitColliders[i].transform.tag == "Interactible")
                {
                    //can do anything in here like different attacks/abilities, cleave
                    Target = hitColliders[i].transform.gameObject;
                    FollowTarget();
                }
                i++;
            }
            FollowPlayer();
        }

        if (wait)
        {
            passive = false;
            aggressive = false;
        }
    }

    void FollowTarget()
    {
        //Face towards Target always
        Vector3 targetPosition = Target.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
        if (distance > 5)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        else if (distance <= 5)
        {
            FollowPlayer();
        }
    }
        public void FollowPlayer()
    {
        //Same basic tracking script we've been using forever
        if (Vector3.Distance(player.position, this.transform.position) < playerTrackingDistance)
        {
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) <= distance)
        {
            this.transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;
        }
    }

    IEnumerator Attack() //When it's at the enemy
    {
        basicAttack = true;

        yield return new WaitForSeconds(.5f);

        Vector3 SpawnSpellLoc = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);

        Instantiate(RangedSpellPrefab, SpawnSpellLoc, Quaternion.identity);
        canAttack = true;
    }

    public void Sacrifice()
    {
        if (canHeal)
        {
            canHeal = false;
            wait = true;

            PlayerStats.Instance.Heal(2f);
            StartCoroutine(Respawn());
        }
    }
    public IEnumerator Respawn()
    {
        yield return new WaitForSeconds(respawnTimer);
        wait = false;
        passive = true;
        canHeal = true;
    }
}



    /*public bool isActive;

    [Header("Player")]
    public Transform player;
    public float playerTrackingDistance;
    public float distance;

    [Header("ObjectFind")]
    public LayerMask CanOpen;
    public float aggroRadius;
    private Collider[] inAggroRadius;
    Collider target;

    [Header("Companion Variables")]
    public float speed;
    private Collider bodyCollider;

    void FixedUpdate()
    {
        if (isActive)
        {
            inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("CanOpen"));
            target = inAggroRadius[0];

            if (inAggroRadius.Length > 0)
            {
                if (Vector3.Distance(target.transform.position, gameObject.transform.position) >= 2)
                {
                    Vector3 targetPosition = target.transform.position;
                    targetPosition.y = transform.position.y;
                    transform.LookAt(targetPosition);

                    float distance = Vector3.Distance(target.transform.position, this.transform.position);
                    if (distance >= 1)
                    {
                        transform.Translate(Vector3.forward * Time.deltaTime * speed);
                    }
                }
                else if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 3)
                {
                    FollowPlayer();
                }
            }

            if (inAggroRadius.Length == 0)
            {
                FollowPlayer();
            }
        }
    }

    public void FollowPlayer()
    {
        //Same basic tracking script we've been using forever
        if (Vector3.Distance(player.position, this.transform.position) < playerTrackingDistance)
        {
            transform.LookAt(player);
            Vector3 direction = player.transform.position - this.transform.position;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }
        if (Vector3.Distance(player.position, this.transform.position) <= distance)
        {
            this.transform.position = (transform.position - player.transform.position).normalized * distance + player.transform.position;
        }*/
