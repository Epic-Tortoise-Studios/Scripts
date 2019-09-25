using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceTransformation : MonoBehaviour
{

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
            if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
            {
                TransformationController.Instance.type = TransformationController.Instance.humanTransformation.type;
                TransformationController.Instance.HumanType();
            }
            else if(TransformationController.Instance.type == TransformationController.TransformationType.HUMAN)
            {
                TransformationController.Instance.type = TransformationController.Instance.ghostTransformation.type;
                TransformationController.Instance.GhostType();

                StartCoroutine(TriggerSet());
            }

        }
    }

    public IEnumerator TriggerSet()
    {
        this.gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        this.gameObject.SetActive(true);
    }
}
