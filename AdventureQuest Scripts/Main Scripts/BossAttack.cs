using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public ParticleSystem damageParticles;
    public GameObject target;
    public float damage;

    public AudioClip laugh;

    public float endTime;
    private float timer;

    private bool onceCheck = false;
    public bool useParticles = true;

    void Start()
    {
        timer = endTime;
    }


    void Update()
    {
        if (GameManager.Instance.bossBattle)
        {
            if (useParticles)
            {
                damageParticles.gameObject.GetComponent<particleAttractorLinear>().target = target.transform;
                damageParticles.Play();
            }

            if (!onceCheck)
            {
                AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
                AudioManager.instance.PlayClip(laugh);

                onceCheck = true;
            }

            timer -= Time.deltaTime;

            if(timer <= 0)
            {
                timer = endTime;
                GameManager.Instance.bossBattle = false;
                if (useParticles)
                {
                    damageParticles.Stop();
                }
                StartCoroutine(CutsceneManager.Instance.animationHelper.GetComponent<AnimationHelper>().EndEncounter());
            }
        }
    }


}
