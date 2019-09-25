using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    [HideInInspector]
    public TMPro.TextMeshProUGUI infoTextObject;
    public string text;

    public void Start()
    {
        infoTextObject = GameObject.FindObjectOfType<InfoTextCheck>().GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            infoTextObject.enabled = true;
            infoTextObject.text = text;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            infoTextObject.enabled = false;
        }
    }
}
