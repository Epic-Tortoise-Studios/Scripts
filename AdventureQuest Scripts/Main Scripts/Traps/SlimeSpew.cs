using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpew : MonoBehaviour
{
    public float speed;
    public float destroyTimer;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }
}
