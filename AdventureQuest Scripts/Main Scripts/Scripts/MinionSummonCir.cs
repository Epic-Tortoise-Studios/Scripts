using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinionSummonCir : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
