using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    private Animator anim;

    private void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (this.gameObject.name == "RedKeyDoor" && KeyManager.Instance.hasRed)
            {
                Debug.Log("Opened Red Door");
                anim.SetBool("Opened", true);
            }
            else if (this.gameObject.name == "BlueKeyDoor" && KeyManager.Instance.hasBlue)
            {
                Debug.Log("Opened Blue Door");
                anim.SetBool("Opened", true);
            }
            else if (this.gameObject.name == "GreenKeyDoor" && KeyManager.Instance.hasGreen)
            {
                Debug.Log("Opened Green Door");
                anim.SetBool("Opened", true);
            }
        }
    }
}
