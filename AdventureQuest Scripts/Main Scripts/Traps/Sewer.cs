using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sewer : MonoBehaviour
{
    public GameObject sewer0;
    public GameObject sewer1;

    private GameObject sewerZero;
    private GameObject sewerOne;
    public float timeBetweenSpews;

    private bool isActive;

    void Start()
    {
        sewerZero = sewer0;
        sewerOne = sewer1;
        isActive = true;
    }

    void Update()
    {
        if(isActive)
        {
            isActive = false;
            StartCoroutine(Spew());
        }
    }

    IEnumerator Spew()
    {
        if(sewer0 != null)
        {
            sewerZero.GetComponent<SpawnSpew>().Spawn();
        }

        yield return new WaitForSeconds(timeBetweenSpews);

        if (sewer1 != null)
        {
            sewerOne.GetComponent<SpawnSpew>().Spawn();
        }

        yield return new WaitForSeconds(timeBetweenSpews);
        isActive = true;
    }
        /*public float timeBetweenSpews;
        public GameObject sewerSpew;

        private bool isActive;

        void Start()
        {
            isActive = true;
        }

        void Update()
        {
           if(isActive)
           {
                isActive = false;
                StartCoroutine(Spew());
           }
        }

        IEnumerator Spew()
        {
            yield return new WaitForSeconds(timeBetweenSpews);
            Instantiate(sewerSpew, transform.position + transform.up * 0, transform.rotation);
            yield return new WaitForSeconds(timeBetweenSpews);
            isActive = true;
        }*/
    }
