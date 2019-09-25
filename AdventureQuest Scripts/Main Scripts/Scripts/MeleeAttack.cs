using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    Animator wepAnim;
    public GameObject wepTrigger;
    public float attkSpeed;

    void Start()
    {
        wepAnim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            wepAnim.SetTrigger("Active");
            wepTrigger.SetActive(true);
            StartCoroutine(TriggerTimer());
        }
    }

    IEnumerator TriggerTimer()
    {
        yield return new WaitForSeconds(attkSpeed);
        wepTrigger.SetActive(false);
    }
}
