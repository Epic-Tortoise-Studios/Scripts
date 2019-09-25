using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundRumble : MonoBehaviour
{
    public GameObject axe;
    public GameObject rumble;
    public float waitToSmash;
    public float waitToReset;

    private bool isActive;

    void Start()
    {
        isActive = false;
    }

    public void OnTriggerEnter(Collider other)
    {
        if(!isActive)
        {
            if (other.tag == "Player")
            {
                isActive = true;
                StartCoroutine(Rumble());
            }
        }
    }

    IEnumerator Rumble()
    {
        rumble.transform.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitToSmash);
        //axe.transform.Rotate(90, 0, 0);
        axe.GetComponent<Animator>().SetBool("slam", true);
        rumble.transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitToReset);
        axe.GetComponent<Animator>().SetBool("slam", false);
        //axe.transform.Rotate(-90, 0, 0);
        isActive = false;
    }
}
