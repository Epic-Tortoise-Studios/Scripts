using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpassableWall : MonoBehaviour
{
    public float damage;

    public bool onEnter;
    public bool onStay;
    public float resetTime;
    private bool isActive;

    void Start()
    {
        isActive = false;
    }
    
    private void OnTriggerEnter(Collider col)
    {
        if(onEnter && !isActive)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                Debug.Log("hitting wall");
                if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
                {
                    PlayerHealth.Instance.TakeGhostDamage(damage);
                }
            }
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (onStay && !isActive)
        {
            isActive = true;
            if (col.gameObject.CompareTag("Player"))
            {
                if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
                {
                    PlayerHealth.Instance.TakeGhostDamage(damage);
                    StartCoroutine(Reset());
                }
            }
        }
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(resetTime);
        isActive = false;
    }
}
