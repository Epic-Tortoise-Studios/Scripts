using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionMovement : MonoBehaviour
{
    [Header("Enemy")]
    public LayerMask enemyLayer;
    public float aggroRadius;
    private Collider[] inAggroRadius;
    Collider target;

    [Header("Companion Variables")]
    public Animator anim;
    public float speed;

    public GameObject explosion;
    public int damage;
    bool canAttack;
    private Rigidbody rb;

    void Start()
    {
        canAttack = true;

        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        inAggroRadius = Physics.OverlapSphere(transform.position, aggroRadius, LayerMask.GetMask("Enemy"));
        target = inAggroRadius[0];
        
        if (inAggroRadius.Length > 0)
        {
            if (Vector3.Distance(target.transform.position, gameObject.transform.position) > 1 && canAttack)
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

            if (Vector3.Distance(target.transform.position, gameObject.transform.position) <= 2 && canAttack)
            {
                canAttack = false;
                Instantiate(explosion, transform.position + (transform.forward * 1), transform.rotation);
                rb.constraints = RigidbodyConstraints.FreezeAll;
                anim.SetBool("isWalking", false);
                anim.SetBool("isIdle", false);
                anim.SetBool("isDead", true);
                StartCoroutine(Dead());
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damage);
        }
    }

    IEnumerator Dead()
    {
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
