using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NecroStates : MonoBehaviour
{
    private NecroMovement necroMovement;
    private GameObject necro;
    public bool isActive;
    
    void Start()
    {
        isActive = false;

        necro = GameObject.FindGameObjectWithTag("NecroCompanion");
        necroMovement = necro.GetComponent<NecroMovement>();
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
    }

    public void Wait()
    {
        necroMovement.wait = true;
        necroMovement.aggressive = false;
        necroMovement.passive = false;
    }

    public void Aggressive()
    {
        necroMovement.aggressive = true;
        necroMovement.passive = false;
        necroMovement.wait = false;
    }
    public void Passive()
    {
        necroMovement.passive = true;
        necroMovement.aggressive = false;
        necroMovement.wait = false;
    }
}
