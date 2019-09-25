using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCFollow : MonoBehaviour
{
    public GameObject thePlayer;
    public float TargetDistance;
    public float AllowedDistance = 5;
    public GameObject TheNPC;
    public float FollowSpeed;
    public RaycastHit Shot;

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(thePlayer.transform);
        if(Physics.Raycast(transform.position,transform.TransformDirection(Vector3.forward),out Shot))
        {
            TargetDistance = Shot.distance;
            if(TargetDistance >= AllowedDistance)
            {
                FollowSpeed = 0.3f;
                //TheNPC.GetComponent<Animation>().Play("Running");
                transform.position = Vector3.MoveTowards(transform.position, thePlayer.transform.position, FollowSpeed);
            }
            else
            {
                FollowSpeed = 0;
                //TheNPC.GetComponent<Animation>().Play("Idle");
            }
        }
    }
}
