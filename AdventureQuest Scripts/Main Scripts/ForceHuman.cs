using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceHuman : MonoBehaviour
{
    public float damage;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
            {
                TransformationController.Instance.type = TransformationController.Instance.humanTransformation.type;
                TransformationController.Instance.HumanType();
            }
            else
            {
                PlayerHealth.Instance.TakePossessedDamage(damage);
            }

        }
    }
}
