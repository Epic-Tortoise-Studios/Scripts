using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamagePlayer : MonoBehaviour
{
    //public Animator PlayerDamaged;
    public AudioSource DamagedPlayer;
    public AudioSource hurtPlayer;

    public float damageTimeout = 4f;
    public bool canTakeDamage = true;
    public float enemyDamage = 1f;

    public Collider hit;
    public Animator enemyanim;
    public GameObject enemyBody;

    // Start is called before the first frame update
    void Start()
    {
        //PlayerDamaged = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        canTakeDamage = true;
        hit = GetComponent<Collider>();
        hit.enabled = false;
        enemyanim = enemyBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerStats.Instance.Health <= 0)
        {
            canTakeDamage = false;
        }

        Invoke("Attacking", 1.5f);
    }

    void Attacking()
    {
        if (enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
                if (hit.enabled == false)
                {
                    hit.enabled = true;
                }
            }

        if(!enemyanim.GetCurrentAnimatorStateInfo(0).IsName("Attack")
            && enemyanim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
            {
            if (hit.enabled == true)
            {
                hit.enabled = false;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (PlayerStats.Instance.Health <= 0 && other.tag == "Player" && hit.enabled == true  && canTakeDamage == true)
            canTakeDamage = false;
        {
            if (other.tag == "Player" && canTakeDamage == true)
            {
                PlayerStats.Instance.TakeDamage(enemyDamage);
                print("Player Trigger Enter Taking Damage");
                hurtPlayer.Play(0);
                DamagedPlayer.Play(0);
                StartCoroutine(damageTimer());
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (PlayerStats.Instance.Health <= 0)
            canTakeDamage = false;
        {
            if (other.tag == "Player" && canTakeDamage == true && hit.enabled == true)
            {
                canTakeDamage = false;
                PlayerStats.Instance.TakeDamage(enemyDamage);
                print("Player Taking Trigger Stay Damage");
                hurtPlayer.Play(0);
                DamagedPlayer.Play(0);
                StartCoroutine(damageTimer());
                print("Damage Off");
                
            }
        }
    }

    IEnumerator damageTimer()
    {
            canTakeDamage = false;
            print("Can Take Damage False");
            yield return new WaitForSeconds(damageTimeout);
            canTakeDamage = true;
            print("Can Take Damage true");
    } 
}
