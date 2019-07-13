using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompSimple : MonoBehaviour
{
    public bool isAttack;
    public bool isFollow;

    private bool canAttack;
    public float speed;

    public GameObject projectile;

    public float fireRate;

    [Header("Player")]
    public Transform player;
    public float playerTrackingDistance;
    public float distance;

    public void Start()
    {
        canAttack = true;
    }

    public void Update()
    {
        if (isAttack)
        {
            StartCoroutine(Attack());
        }
        else
        {
            FollowPlayer();
        }
    }
    IEnumerator Attack()
    {
        if(isAttack)
        {
            if (canAttack)
            {
                canAttack = false;
                isAttack = false;
                Instantiate(projectile, transform.position + (transform.forward * 1), transform.rotation);
                yield return new WaitForSeconds(fireRate);
                canAttack = true;
                isAttack = true;
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
        }
    }

}
