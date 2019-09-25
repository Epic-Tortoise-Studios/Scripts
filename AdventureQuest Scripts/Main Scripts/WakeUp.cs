using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WakeUp : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    public GameObject ColliderObject;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_Rigidbody.sleepThreshold = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(ColliderObject)
        {
            StartCoroutine(Lock());
        }
    }

    IEnumerator Lock()
    {
        yield return new WaitForSeconds(3);
        m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }
}
