using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightSlime : MonoBehaviour
{
    public float speed;
    public float destroyTimer;
    public float damage;

    void Start()
    {
        StartCoroutine(Destroy());
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(destroyTimer);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerStats.Instance.TakeDamage(damage);
            Destroy(this.gameObject);
        }
            Destroy(gameObject);
    }
}
