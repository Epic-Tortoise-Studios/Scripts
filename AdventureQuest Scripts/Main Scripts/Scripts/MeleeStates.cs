using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeleeStates : MonoBehaviour
{
    private CompMoveTest compMoveTest;
    private GameObject melee;
    public bool isActive = false;

    public void Start()
    {
        melee = GameObject.FindGameObjectWithTag("MeleeCompanion");
        compMoveTest = melee.GetComponent<CompMoveTest>();
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
            if(isActive == true)
            {
                Sacrifice();
            }
        }

    }
    public void Wait()
    {
        compMoveTest.wait = true;
        compMoveTest.aggressive = false;
        compMoveTest.passive = false;
    }

    public void Aggressive()
    {
        compMoveTest.aggressive = true;
        compMoveTest.passive = false;
        compMoveTest.wait = false;
    }

    public void Passive()
    {
        compMoveTest.passive = true;
        compMoveTest.aggressive = false;
        compMoveTest.wait = false;
    }

    public void Sacrifice()
    {
        compMoveTest.wait = true;
        compMoveTest.aggressive = false;
        compMoveTest.passive = false;
        compMoveTest.Sacrifice();
    }
}
