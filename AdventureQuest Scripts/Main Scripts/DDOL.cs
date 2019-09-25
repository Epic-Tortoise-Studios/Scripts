using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    static bool spawned;

    void Start()
    {
        if (spawned == false)
        {
            spawned = true;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    void Awake()
    {
        //Leave this empty and try that.
    }
}
