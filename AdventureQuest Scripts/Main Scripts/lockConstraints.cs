using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lockConstraints : MonoBehaviour
{
    Rigidbody m_Rigidbody;
    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Mana")
        {
            m_Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
