using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPoison : MonoBehaviour
{
    public float timer;

    // Start is called before the first frame update
    /*void Awake()
    {
        Destroy(gameObject, Destroytimer);
    }*/

    // Update is called once per frame
    void Update()
    {
        timer -= 1 * Time.deltaTime;

        if (timer <0)
        {
            Destroy(this.gameObject);
        }
    }
}
