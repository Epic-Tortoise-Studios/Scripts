using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionTrigger : MonoBehaviour
{
    private MeleeStates meleeStates;
    public GameObject meleeStateScript;
    private MeleeMovement meleeMovement;
    public GameObject melee;

    private bool inTrigger;


    void Start()
    {
        meleeStates = meleeStateScript.GetComponent<MeleeStates>();
        meleeMovement = melee.GetComponent<MeleeMovement>();
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            inTrigger = true;

        }
    }

    private void PlayerInput()
    {
        if (inTrigger)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                meleeStates.isActive = true;
                meleeMovement.isActive = true;
            }
        }
        
    }
}
