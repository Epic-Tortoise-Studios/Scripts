using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasterStates : MonoBehaviour
{
    private CasterMovement casterMovement;
    private GameObject caster;
    public bool isActive = false;

    public void Start()
    {
        caster = GameObject.FindGameObjectWithTag("CasterCompanion");
        casterMovement = caster.GetComponent<CasterMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            if (isActive == true)
            {
                Aggressive();
            }
        }

        if (Input.GetKeyDown("2"))
        {
            if (isActive == true)
            {
                Passive();
            }
        }

        if (Input.GetKeyDown("3"))
        {
            if (isActive == true)
            {
                Wait();
            }
        }

        if (Input.GetKeyDown("4"))
        {
            if (isActive == true)
            {
                Heal();
            }
        }
    }
    public void Wait()
    {
        casterMovement.wait = true;
        casterMovement.aggressive = false;
        casterMovement.passive = false;
    }

    public void Aggressive()
    {
        casterMovement.aggressive = true;
        casterMovement.passive = false;
        casterMovement.wait = false;
    }
    public void Passive()
    {
        casterMovement.passive = true;
        casterMovement.aggressive = false;
        casterMovement.wait = false;
    }

    public void Heal()
    {
        casterMovement.HealPlayer();
    }
}
