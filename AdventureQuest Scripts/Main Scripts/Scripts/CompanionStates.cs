using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompanionStates : MonoBehaviour
{
    private HealerMovement healerMovement;
    private GameObject healer;
    public bool isActive = false;


    public void Start()
    {
        healer = GameObject.FindGameObjectWithTag("HealthCompanion");
        healerMovement = healer.GetComponent<HealerMovement>();
        healerMovement.healing = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown("1"))
        {
            if (isActive == true)
            {
                Aggressive();
            }
        }

        if (Input.GetKeyDown("3"))
        {
            if (isActive == true)
            {
                Passive();
            }
        }

        if (Input.GetKeyDown("4"))
        {
            if (isActive == true)
            {
                Follow();
            }
        }

        if(Input.GetKeyDown("5"))
        {
            if(isActive == true)
            {
                Wait();
            }
        }
    }

    public void Aggressive()
    {
        healerMovement.aggressive = true;
        healerMovement.follow = true;
        healerMovement.wait = false;
        healerMovement.passive = false;
    }

    public void Wait()
    {
        healerMovement.wait = true;
        healerMovement.follow = false;
        healerMovement.passive = false;
        healerMovement.aggressive = false;
    }

    public void Passive()
    {
        healerMovement.passive = true;
        healerMovement.follow = true;
        healerMovement.wait = false;
        healerMovement.aggressive = false;
    }

    public void Follow()
    {
        healerMovement.aggressive = false;
        healerMovement.follow = true;
        healerMovement.passive = false;
        healerMovement.wait = false;
    }
}
