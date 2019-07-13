using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionPointer : MonoBehaviour
{
    public float playerTrackingDistance;
    public Transform player;
    public float speed;
    public float distance;

    public GameObject Target;
    public float aggroRadius;

    void Update()
    {
        if (Target == null)
        {
            SearchForTarget();
            FollowPlayer();
        }
        if (Target != null)
        {
            FollowTarget();
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

    void SearchForTarget()
    {
        Vector3 center = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
        Collider[] hitColliders = Physics.OverlapSphere(center, aggroRadius);
        int i = 0;
        while (i < hitColliders.Length)
        {
            if (hitColliders[i].transform.tag == "Chest")
            {
                //can do anything in here like different attacks/abilities, cleave
                Target = hitColliders[i].transform.gameObject;
            }
            i++;
        }
    }

    void FollowTarget()
    {
        //Face towards Target always
        Vector3 targetPosition = Target.transform.position;
        targetPosition.y = transform.position.y;
        transform.LookAt(targetPosition);

        float distance = Vector3.Distance(Target.transform.position, this.transform.position);
        if (distance > 10)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        if(distance <= 10)
        {
            FollowPlayer();
        }
    }
}
