using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonDamage : MonoBehaviour
{

    public float damage;
    public bool canTakeDamage;
    public BoxCollider bCollider;
    public float Destroytimer;

    // Start is called before the first frame update
    void Start()
    {
        canTakeDamage = true;
        bCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        Destroytimer -= 1 * Time.deltaTime;

        if(Destroytimer <= 0)
        {
            Destroy(bCollider);
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && canTakeDamage == true)
        {
            PlayerStats.Instance.TakeDamage(damage);
            Debug.Log("PlayerHurt");
            Destroy(bCollider);
        }
    }

}
