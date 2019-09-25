/*
 *  Author: ariel oliveira [o.arielg@gmail.com]
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerHealth : MonoBehaviour
{
    public delegate void OnHealthChangedDelegate();
    public OnHealthChangedDelegate onHealthChangedCallback;

    #region Sigleton
    private static PlayerHealth instance;
    public static PlayerHealth Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<PlayerHealth>();
            return instance;
        }
    }
    #endregion


    #region Health Variables
    [SerializeField]
    private float health;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float maxTotalHealth;

    public float Health { get { return health; } }
    public float MaxHealth { get { return maxHealth; } }
    public float MaxTotalHealth { get { return maxTotalHealth; } }

    public Animator anim;

    public AudioClip death;
    public AudioClip hurt;
    public AudioMixerGroup SFX;

    private bool isDead;
    public bool isRespawning;



    #endregion

    public void Update()
    {
        Death();
    }

    public void Heal(float health)
    {
        this.health += health;
        ClampHealth();
    }

    public void CompanionHeal(int healAmount)
    {
        health += healAmount;
        ClampHealth();
    }

    public void TakeDamage(float dmg)
    {
        if(TransformationController.Instance.type != TransformationController.TransformationType.GHOST && !TransformationController.Instance.possessedEnemy)
        {
            health -= dmg;
            ClampHealth();
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(hurt);
        }      
    }

    public void TakeGhostDamage(float dmg)
    {
        if (TransformationController.Instance.type == TransformationController.TransformationType.GHOST)
        {
            health -= dmg;
            ClampHealth();
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(hurt);
        }
    }

    public void TakePossessedDamage(float dmg)
    {
        if (TransformationController.Instance.possessedEnemy)
        {
            health -= dmg;
            ClampHealth();
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(hurt);
        }
    }

    public void AddHealth()
    {
        if (maxHealth < maxTotalHealth)
        {
            maxHealth += 1;
            health = maxHealth;

            if (onHealthChangedCallback != null)
                onHealthChangedCallback.Invoke();
        }
    }

    public void Death()
    {
        if (health <= 0 && isDead == false)
        {
            Debug.Log("Player Died");
            //Added 6/13/2019
            ClampHealth();
            StartCoroutine(DeathBehavior());

        }
        else if (health > 0 && TransformationController.Instance.type != TransformationController.TransformationType.GHOST)
        {
            //Added 6/13/2019
            isDead = false;
            isRespawning = false;
            StopCoroutine(DeathBehavior());

        }
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);

        if (onHealthChangedCallback != null)
            onHealthChangedCallback.Invoke();
    }

    public IEnumerator DeathBehavior()
    {
        if (TransformationController.Instance.type != TransformationController.TransformationType.GHOST && !TransformationController.Instance.possessedEnemy)
        {
            anim.SetBool("isDead", true);
            isRespawning = true;
            GameManager.Instance.CursorLock();
            PlayerController.Instance.canMove = false;
            Debug.Log(PlayerController.Instance.canMove);
            //CameraController.Instance.FadeIn();
            isDead = true;
            AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(death);
            yield return new WaitForSeconds(4);
            //CameraController.Instance.FadeOut();

            anim.SetBool("isDead", false);
            if (GameManager.Instance.playerLastPos == new Vector3(0, 0, 0))
            {
                this.transform.position = GameManager.Instance.initialPlayerPos;
            }
            else
            {
                this.transform.position = GameManager.Instance.playerLastPos;
            }
            Heal(maxHealth);
            PlayerController.Instance.canMove = true;
        }
       
    }
}
