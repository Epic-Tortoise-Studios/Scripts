using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    #region Sigleton
    private static CombatController instance;
    public static CombatController Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<CombatController>();
            return instance;
        }
    }
    #endregion

    public GameObject target;

    public LayerMask layerMask;

    public bool inCombat;
    private bool canTarget;

    void Start()
    {
        
    }


    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 10, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            canTarget = true;

            target = hit.collider.gameObject;

            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 10, Color.blue);
            Debug.Log("Did not Hit");
        }

        
    }

    void Engage()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (!inCombat)
            {

            }
        }
    }

    //For eventual zelda-esque z-targetting
    void Targetting()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            //PlayerController.Instance.pivot.transform.position = target.transform.position;
            //PlayerController.Instance.pivot.transform.parent = target.transform;
            //transform.LookAt(target.transform.position);

            inCombat = true;
        }
    }

}
