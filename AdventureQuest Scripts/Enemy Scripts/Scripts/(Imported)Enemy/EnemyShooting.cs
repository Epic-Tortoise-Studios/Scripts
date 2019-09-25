using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public float maximumDamage = 1f;
    public float minimumDamage = 0.25f;
    public AudioClip shotClip;
    public float flashIntensity = 3f;
    public float fadeSpeed = 10f;

    private Animator anim;
    private HashIDs hash;
    private LineRenderer laserShotLine;
    private Light laserShotLight;
    private SphereCollider col;
    private Transform player;
    private PlayerStats playerHealth;
    private bool shooting;
    private float scaledDamage;

    void Awake()
    {
        anim = GetComponent<Animator>();
        laserShotLine = GetComponentInChildren<LineRenderer>();
        laserShotLight = laserShotLine.gameObject.GetComponent<Light>();
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.gameObject.GetComponent<PlayerStats>();
        hash = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<HashIDs>();
    } 
}
