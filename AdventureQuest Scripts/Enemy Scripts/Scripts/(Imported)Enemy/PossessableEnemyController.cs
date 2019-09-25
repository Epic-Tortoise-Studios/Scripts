using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PossessableEnemyController : MonoBehaviour
{
    private PlayerController playerController;
    private PlayerAbilities playerAbilities;
    private EnemySpeedAbility enemySpeedAbility;
    private Animator anim;

    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        playerAbilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
        enemySpeedAbility = gameObject.GetComponentInChildren<EnemySpeedAbility>();
    }

    void Update()
    {
        PlayerInput();
    }

    void PlayerInput()
    {
        if (TransformationController.Instance.possessedEnemy)
        {
            if (TransformationController.Instance.insideEnemy)
            {
                anim.SetBool("isPossessed", true);

                if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
                {
                    anim.SetBool("isRunning", true);
                }
                else
                {
                    anim.SetBool("isRunning", false);
                }
            }
            anim.SetBool("isPossessed", true);
        }
        else
        {
            anim.SetBool("isPossessed", false);
            anim.SetBool("isRunning", false);
        }
    }
}
