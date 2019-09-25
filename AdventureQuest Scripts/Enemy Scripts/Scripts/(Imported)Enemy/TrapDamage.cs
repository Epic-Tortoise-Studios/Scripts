using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour
{
    public float trapDamage;
    public bool canGiveDamage;

    //Timeout should be set to 0.5 in inspector
    public float damageTimeout;

    // Start is called before the first frame update
    void Start()
    {
        canGiveDamage = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (canGiveDamage == true)
            {
                Debug.Log("Colliding With Trap");
                PlayerStats.Instance.TakeDamage(trapDamage);
                canGiveDamage = false;
                StartCoroutine(damageTimer());
            }
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (canGiveDamage == true)
            {
                Debug.Log("Colliding With Trap");
                PlayerStats.Instance.TakeDamage(trapDamage);
                canGiveDamage = false;
                StartCoroutine(damageTimer());
            }
        }
    }
    */

    IEnumerator damageTimer()
    {
        canGiveDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canGiveDamage = true;
    }
}
