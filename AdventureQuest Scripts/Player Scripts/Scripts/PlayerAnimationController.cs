using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    //private PlayerAbilities playerAbilities;

    private Animator anim;

    //private GameObject beastFist;
    private Collider playerSword;

    private bool shouldNotMove;
    private bool animationEnd;

    void Start()
    {
        //playerAbilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
        //playerCombat = GameObject.FindGameObjectWithTag("Weapon").GetComponent<PlayerCombat>();

        //beastFist = playerAbilities.beastFist;
        //playerSword = playerCombat.weapon.GetComponent<Collider>();


        anim = this.gameObject.GetComponent<Animator>();
    }

    public void ShouldNotMove()
    {
        PlayerController.Instance.canMove = false;
        Debug.Log("Should Not Move");
    }

    public void ShouldMove()
    {
        PlayerController.Instance.canMove = true;
        Debug.Log("Should Move");
    }

    public void ShouldNotAttack()
    {
        PlayerController.Instance.canAttack = false;
        Debug.Log("Should Not Attack");
    }

    public void ShouldAttack()
    {
        PlayerController.Instance.canAttack = true;
        Debug.Log("Should Attack");
    }

    public void ApplyRootMotion()
    {
        anim.applyRootMotion = true;
    }

    public void DisableRootMotion()
    {
        anim.applyRootMotion = false;
    }

    public void FadeIn()
    {
        //CameraController.Instance.FadeIn();
    }

    public void FadeOut()
    {
        //CameraController.Instance.FadeOut();
    }

    //Turn on when Playerabilities is Renabled
    public void BeastFistOn()
    {
        if (TransformationController.Instance.type == TransformationController.TransformationType.BEAST)
        {
            TransformationController.Instance.beastFist.SetActive(true);
        }
    }

    public void BeastFistOff()
    {
        if (TransformationController.Instance.type == TransformationController.TransformationType.BEAST)
        {
            TransformationController.Instance.beastFist.SetActive(false);
        }
    }

    /*public void SwordColliderOn()
    {
        playerSword.enabled = true;
        
    }

    public void SwordColliderOff()
    {
        playerSword.enabled = false;

    }*/
}
