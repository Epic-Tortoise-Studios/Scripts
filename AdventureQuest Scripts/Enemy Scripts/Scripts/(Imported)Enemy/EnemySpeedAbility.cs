using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpeedAbility : MonoBehaviour
{

    private PlayerAbilities playerAbilities;

    private GameObject player;
    public GameObject enemy;

    private bool possessable;
    private bool speedAbility;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerAbilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
    }

    void Update()
    {
        Possessable();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
            {
                possessable = true;
                TransformationController.Instance.infoText.text = "Press E To Interact";
                TransformationController.Instance.infoText.enabled = true;
            }
            else
            {
                TransformationController.Instance.infoText.text = "Must Be Ghost To Interact";
                TransformationController.Instance.infoText.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            possessable = false;
            TransformationController.Instance.infoText.enabled = false;
            Debug.Log("OnTriggerExit");
        }

    }

    void Possessable()
    {
        if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
        {
            if (possessable)
            {
                if (Input.GetKey(KeyCode.E))
                {
                    TransformationController.Instance.possessedEnemy = true;
                    //playerAbilities.speedAbility = true;
                    TransformationController.Instance.possessableEnemy = enemy;
                    TransformationController.Instance.StartCoroutine(TransformationController.Instance.PossessingEnemy());
                    //possessing = true;
                }
            }
        }
    }
}
