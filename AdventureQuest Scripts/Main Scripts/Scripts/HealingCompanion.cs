using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingCompanion : MonoBehaviour
{

    private Transform player;
    private GameObject playerObject;
    private Rigidbody rb;
    public PlayerHealth playersHealth;
    public GameObject healthPack;


    public bool wait;
    public float speed;
    public float playerTrackingDistance;
    public float distance;
    public float timer;
    private float hurt;
    public bool healing;

 


    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.transform;
        // playersHealth = playerObject.GetComponent<PlayerHealth>();
        // currentHealth = playersHealth.GetComponent<PlayerHealth>().currentHealth;
        hurt = playerObject.GetComponent<PlayerHealth>().Health;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(PlayerHealth.Instance.Health);

        if (PlayerHealth.Instance.Health < 2 )
        {
            //healing = true;
            // PlayerStats.Instance.Heal(1.5f);

            timer += Time.deltaTime;
            if(timer >= 5)
            {
                healing = true;
                timer = 0;
            }
        }
    

        else
        {
            healing = false;
            FollowPlayer();
        }

        if (healing)
        {
            PlayerHealth.Instance.Heal(1.5f);
        }
    }

    public void FollowPlayer()
    {
        wait = false;
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
