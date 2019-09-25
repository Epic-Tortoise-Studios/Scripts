using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartContainer : MonoBehaviour
{
    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.LookAt(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.gameObject.name == "HeartContainerPickup")
            {
                PlayerHealth.Instance.AddHealth();
                Destroy(this.gameObject);
            }
            else if(this.gameObject.name == "FullHealPickup")
            {
                PlayerHealth.Instance.Heal(1);
                Destroy(this.gameObject);
            }
            else if (this.gameObject.name == "HalfHealPickup")
            {
                PlayerHealth.Instance.Heal(.5f);
                Destroy(this.gameObject);
            }
            else if (this.gameObject.name == "QuarterHealPickup")
            {
                PlayerHealth.Instance.Heal(.25f);
                Destroy(this.gameObject);
            }
        }
    }
}
