using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritBehavior : MonoBehaviour
{
    private GameObject player;
    private GameObject target;
    private Rigidbody rb;
    public GameObject warningImage;
    public ParticleSystem damageParticles;

    public float forceAmount;
    private bool tooClose;

    public float amplitude = 0.5f;
    public float frequency = 1f;
    public float maxDistance;
    public float damage;
    public float damageTime;
    private float counter;

    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();

    public LayerMask layerMask;
    

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = this.gameObject.GetComponent<Rigidbody>();

        warningImage.SetActive(false);
        damageParticles.Stop();
        counter = damageTime;
    }


    void Update()
    {
        if(Time.timeScale == 1)
        {
            posOffset = transform.position;

            Vector3 direction = (this.transform.position - player.transform.position).normalized;
            if (tooClose)
            {
                rb.AddForce(direction * forceAmount);
            }
            else
            {
                rb.velocity = new Vector3(0, 0, 0);
                tempPos = posOffset;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

                transform.position = tempPos;
            }

            transform.LookAt(player.transform.position);

            LookAtPlayer();
            DamagePlayer();
        }
        else
        {
            rb.velocity = new Vector3(0, 0, 0);
        }
    }
    
    void LookAtPlayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, maxDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);

            target = hit.collider.gameObject;

            if(target == player)
            {
                counter -= Time.deltaTime;
                warningImage.SetActive(true);
            }

            Debug.Log("Ghost is looking at: " + target.name);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * maxDistance, Color.blue);
            counter = damageTime;
            warningImage.SetActive(false);
        }
    }

    void DamagePlayer()
    {
        if(counter <= 0)
        {
            PlayerHealth.Instance.TakeGhostDamage(damage);
            counter = damageTime;
            StartCoroutine(GhostAttack());
        }
        else
        {
            StopCoroutine(GhostAttack());
        }
    }

    #region TriggerBehaviors
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            tooClose = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            tooClose = false;
        }
    }
    #endregion

    #region Coroutines
    public IEnumerator GhostAttack()
    {
        damageParticles.Play();
        yield return new WaitForSeconds(.5f);
        damageParticles.Stop();
        yield break;
    }
    #endregion
}
