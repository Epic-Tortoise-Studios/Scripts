using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DAT : MonoBehaviour
{
    public float timer;

   void Awake()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timer);
        Destroy(this.gameObject);
    }
}
