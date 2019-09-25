using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAura : MonoBehaviour
{
    public int heal;
    public int healDelay;

    public void OnTriggerEnter()
    {
        StartCoroutine(HealPlayer());
    }

    IEnumerator HealPlayer()
    {
        PlayerHealth.Instance.Heal(heal);
        yield return new WaitForSeconds(healDelay);
        PlayerHealth.Instance.Heal(heal);
        yield return new WaitForSeconds(healDelay);
        PlayerHealth.Instance.Heal(heal);
        yield return new WaitForSeconds(healDelay);
        Destroy(gameObject);
    }
}

