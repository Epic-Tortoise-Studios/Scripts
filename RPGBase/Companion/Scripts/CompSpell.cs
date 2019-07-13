using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSpell : MonoBehaviour
{
    [Header("Enemy")]
    public LayerMask enemyLayer;
    public float aggroRadius;
    private Collider[] inAggroRadius;
    Collider target;
    public EnemyStats enemyStatsScript;

    [Header("Companion Variables")]
    public Animator anim;
    public float speed;

    public GameObject explosion;
    public int damage;
    bool canAttack;
    private Rigidbody rb;

    private float destroyTimer;

    void Start()
    {
        canAttack = true;
        destroyTimer = 4.0f;

        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        destroyTimer -= 1 * Time.deltaTime;

        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));
        target = inAggroRadius[0];

        if (inAggroRadius.Length > 0)
        {
            enemyStatsScript = target.transform.gameObject.transform.GetComponent<EnemyStats>();

            if (Vector3.Distance(target.transform.position, gameObject.transform.position) > 1 && canAttack)
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

            if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 10)
            {
                enemyStatsScript.RecieveDamage(10);
                Destroy(this.gameObject);
            }
        }
        else if (target = null)
        {
            Destroy(this.gameObject);
        }
    }
}
