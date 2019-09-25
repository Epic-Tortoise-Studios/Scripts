using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    //public Animator a_Attack1;

    public float attackspeed = 1.5f;
    public GameObject weapon;
    public Animator combat;
    public GameObject Player;

    public PlayerStats TakeDamage;

    public Collider DamageZone;

    public bool canAttack;
    public float damageTimeout = 1f;
    public int damageToGive = 20;
    public PlayerStats health;
    //public Animator WeaponDying;
    //public PlayerStats playerscript;



    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        weapon = GameObject.Find("PlayerSwordTrigger");
        combat = GetComponentInParent<Animator>();
        TakeDamage = Player.GetComponent<PlayerStats>();
        DamageZone = weapon.GetComponent<Collider>();
        health = GetComponentInParent<PlayerStats>();
        PlayerStats playerscript = Player.GetComponent<PlayerStats>();
        DamageZone.enabled = !DamageZone.enabled;
        canAttack = true;
    }

    void Update()
    {
            Attacking();
    }

    public void Attacking()
    {
        if (combat.GetCurrentAnimatorStateInfo(0).IsName("Attack") 
            && combat.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            if (DamageZone.enabled == false)
            {
                DamageZone.enabled = true;
                print("Player is Attacking");
            }
        }
        else
        {
                canAttack = true;
                DamageZone.enabled = false;           
        }
    }

    public void NotAttacking()
    {
            canAttack = true;
            DamageZone.enabled = false;
    }
    
    public void AttackingNow()
    {
        canAttack = false;
        
    }

    public void NoMoreAttacking()
    {
        canAttack = true;
        //DamageZone.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy" && DamageZone.enabled == true && canAttack == true && 
            combat.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && combat.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
        {
            canAttack = false;
            other.gameObject.GetComponent<EnemyHealth>().HurtEnemy(damageToGive);
            Invoke("NoMoreAttacking", 1);
            print("Damaged Enemy");
            //StartCoroutine(damageTimer());                    
        }
    }
    public void damageCheck()
    {
        if (PlayerStats.Instance.Health <= 0)
        {
            Dying();
        }
    }

    public void Dying()
    {
        combat.SetBool("isDead", true);
    }

    public IEnumerator damageTimer()
    {
        canAttack = false;
        yield return new WaitForSeconds(damageTimeout);
        print("Running CoRoutine");
        canAttack = true;
    }


}
