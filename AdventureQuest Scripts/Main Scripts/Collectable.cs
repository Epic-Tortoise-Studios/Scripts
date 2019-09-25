using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int collectableValue;
    public AudioClip pickup;
    public AudioSource audioSource;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Picked Up Collectable");
            GameManager.Instance.AddCollectableCount(collectableValue);
            //AudioManager.instance.audioSource = this.gameObject.GetComponent<AudioSource>();
            AudioManager.instance.PlayClip(pickup);

            this.gameObject.SetActive(false);
        }
    }
}
