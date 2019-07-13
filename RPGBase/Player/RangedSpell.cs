using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedSpell : MonoBehaviour
{
    public GameObject Target;
    public GameObject player;
    private float destroyTimer;
    
    void Start()
    {
        destroyTimer = 9f;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        destroyTimer -= 0.2f;
        if (Target != null)
        {
            Vector3 targetPosition = new Vector3(Target.transform.position.x,
            Target.transform.position.y,
            Target.transform.position.z);

            this.transform.LookAt(targetPosition);

            float distance2 = Vector3.Distance(Target.transform.position, this.transform.position);

            if (destroyTimer > 0)
            {
                if (distance2 > 2.0f)
                {
                    transform.Translate(Vector3.forward * 30.0f * Time.deltaTime);
                }
                else
                {
                    HitTarget();
                }
            }
        }
        if (destroyTimer <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    void HitTarget()
    {
        player.GetComponent<UserStats>().BasicAttack();
        Destroy(this.gameObject);
    }
}
