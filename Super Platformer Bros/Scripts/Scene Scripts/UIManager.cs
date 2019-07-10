using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //For Health Bar
    private PlayerHealth playerHealth;
    public Slider healthBar;
    private float health;
    //public Coin_Pickup_Script coinPickup;
    //public string countCoins;
    //public int coinNumber;

    

    //For Abilities
    private PlayerAbilities playerAbilities;
    public Text currentPowerText;
    public Image currentPowerImage;
    //public Text coinCount;

    public Sprite jumpSprite;
    public Sprite shootSprite;
    public Sprite speedSprite;
    public Sprite invulSprite;
    public Sprite nullSprite;

    void Start()
    {
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        playerAbilities = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAbilities>();
       // countCoins = coinNumber.ToString();
       // coinNumber = coinPickup.CoinCount;
    }

    void Update()
    {
        HealthCheck();
        AbilityCheck();
        //CoinCheck();
        //PlayerPrefs.GetInt("Coins", coinPickup.CoinCount);
    }

  /*  private void coinCheck()
    {
        coinNumber = coinPickup.CoinCount;
        countCoins = coinNumber.ToString();
        coinCount.text = countCoins;
    }
*/
    private void HealthCheck()
    {
        healthBar.maxValue = playerHealth.maxHealth;
        health = playerHealth.currentHealth;
        healthBar.value = health;
    }

    private void AbilityCheck()
    {
        if (playerAbilities.jumpPower)
        {
            currentPowerImage.sprite = jumpSprite;
            currentPowerText.text = "Super Jump";
        }
        else if (playerAbilities.shootPower)
        {
            currentPowerImage.sprite = shootSprite;
            currentPowerText.text = "Fireball Shoot";
        }
        else if (playerAbilities.speedPower)
        {
            currentPowerImage.sprite = speedSprite;
            currentPowerText.text = "Super Speed";
        }
        else if (playerAbilities.invulnerablePower)
        {
            currentPowerImage.sprite = invulSprite;
            currentPowerText.text = "Invulnerability";
        }
        /*else if(playerAbilities.jumpPower == false && playerAbilities.shootPower == false && playerAbilities.speedPower == false && playerAbilities.invulnerablePower == false)
        {
            currentPowerImage.sprite = null;
        }*/
        else if (currentPowerImage.sprite == null)
        {
            currentPowerImage.sprite = nullSprite;
            currentPowerText.text = "No Powers";
        }
    }

    void CoinCheck()
    {
        //coinNumber = PlayerPrefs.GetInt("Coins");
        //Debug.Log(PlayerPrefs.GetInt("Coins"));
    }



}
