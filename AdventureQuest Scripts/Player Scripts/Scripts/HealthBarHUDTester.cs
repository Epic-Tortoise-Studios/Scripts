/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using UnityEngine;

public class HealthBarHUDTester : MonoBehaviour
{
    public void AddHealth()
    {
        PlayerHealth.Instance.AddHealth();
    }

    public void Heal(float health)
    {
        PlayerHealth.Instance.Heal(health);
    }

    public void Hurt(float dmg)
    {
        PlayerHealth.Instance.TakeDamage(dmg);
    }
}
