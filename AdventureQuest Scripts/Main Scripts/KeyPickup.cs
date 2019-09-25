using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(this.gameObject.name == "RedKey")
            {
                Debug.Log("Picked Up Red Key");
                KeyManager.Instance.PickedUpRed();
                Destroy(this.gameObject);
            }
            else if(this.gameObject.name == "BlueKey")
            {
                Debug.Log("Picked Up Blue Key");
                KeyManager.Instance.PickedUpBlue();
                Destroy(this.gameObject);
            }
            else if (this.gameObject.name == "GreenKey")
            {
                Debug.Log("Picked Up Green Key");
                KeyManager.Instance.PickedUpGreen();
                Destroy(this.gameObject);
            }
        }
    }
}
