using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectReferences : MonoBehaviour
{
    #region Singleton
    private static ObjectReferences instance;
    public static ObjectReferences Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<ObjectReferences>();
            return instance;
        }
    }
    #endregion

    //Access Scene Objects Here!
    public GameObject testObject;
    public GameObject animationObject;

    private void Start()
    {

    }

  
}
