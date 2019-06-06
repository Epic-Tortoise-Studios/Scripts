using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendulum : MonoBehaviour
{

    public GameObject currentPlayer;
    private PlayerHealth playerHealth;
    public GameObject body;
    public int damage;

    void Start()
    {
        currentPlayer = GameObject.FindGameObjectWithTag("Player");
        playerHealth = currentPlayer.GetComponent<PlayerHealth>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHealth.currentHealth -= damage;
            Debug.Log("10 damage");
            //body.SetActive(true);
            //currentPlayer.transform.parent = this.gameObject.transform;
        }
       
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            currentPlayer.transform.parent = null;
        }
    }
}
