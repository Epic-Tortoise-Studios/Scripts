using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    private GameObject player;

    //For Health Tests
    public float damage;
    public float heal;

    //For Stat Display
    public TMPro.TextMeshProUGUI healthText;
    public TMPro.TextMeshProUGUI movementText;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        Stats();
    }
    
    public void HurtPlayer()
    {
        PlayerHealth.Instance.TakeDamage(damage);
        Debug.Log("Hurting Player for " + damage + " damage");
    }

    public void HealPlayer()
    {
        PlayerHealth.Instance.Heal(heal);
        Debug.Log("Healing Player for " + heal + " health");
    }

    public void AddHealth()
    {
        PlayerHealth.Instance.AddHealth();
        Debug.Log("Added another heart");
    }
    

    public void ResetScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
        Time.timeScale = 1f;
    }

    public void AddSoul()
    {
        GameManager.Instance.AddCollectableCount(1);
    }

    public void C1()
    {
        player.transform.position = GameManager.Instance.checkpoint1.transform.position;
    }

    public void C2()
    {
        player.transform.position = GameManager.Instance.checkpoint2.transform.position;
    }

    public void C3()
    {
        player.transform.position = GameManager.Instance.checkpoint3.transform.position;
    }
    public void C4()
    {
        player.transform.position = GameManager.Instance.checkpoint4.transform.position;
    }
    public void C5()
    {
        player.transform.position = GameManager.Instance.checkpoint5.transform.position;
    }
    public void C6()
    {
        player.transform.position = GameManager.Instance.checkpoint6.transform.position;
    }

    public void Stats()
    {

        healthText.text = ("Max Total Health: " + PlayerHealth.Instance.MaxTotalHealth + "\nCurrent Max Health: " + PlayerHealth.Instance.MaxHealth + "\nHealth: " + PlayerHealth.Instance.Health + "\nSouls: " + GameManager.Instance.currentCollectableCount);

        movementText.text = ("Current Speed: " + PlayerController.Instance.currentSpeed + "\nSprint Speed: " + PlayerController.Instance.sprintSpeed);
    }


    


}
