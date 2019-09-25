using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStamina : MonoBehaviour
{
    #region Singleton
    private static PlayerStamina instance;
    public static PlayerStamina Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerStamina>();
            return instance;
        }
    }
    #endregion

    private Slider staminaBar;

    public float maxStamina;
    [HideInInspector]
    public float stamina;
    public float maxRegen;
    public float extraRegen;
    private float regen;

    void Start()
    {
        staminaBar = GameObject.FindObjectOfType<StaminaCheck>().GetComponent<Slider>();
        staminaBar.maxValue = maxStamina;
        stamina = maxStamina;
    }


    void Update()
    {
        stamina += regen * Time.deltaTime;
        staminaBar.value = stamina;

        if (stamina > maxStamina)
        {
            stamina = maxStamina;
        }
        else if(stamina < 0)
        {
            stamina = 0;
        }

        if(PlayerController.Instance.controller.velocity == new Vector3(0,0,0) && stamina < maxStamina)
        {
            regen = extraRegen;
        }
        else
        {
            regen = maxRegen;
        }
    }

    public void SubtractStamina(float sum)
    {
        stamina -= sum;
    }
}
