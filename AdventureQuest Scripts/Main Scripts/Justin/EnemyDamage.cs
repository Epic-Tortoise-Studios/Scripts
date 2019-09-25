using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{

    public float damage;
    public bool canTakeDamage;
    public float damageTimeout = 2f;

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && canTakeDamage == true)
        {
            canTakeDamage = false;
            PlayerStats.Instance.TakeDamage(damage);
            Debug.Log("PlayerHurt");

            StartCoroutine(damageTimer());
        }
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }

}
