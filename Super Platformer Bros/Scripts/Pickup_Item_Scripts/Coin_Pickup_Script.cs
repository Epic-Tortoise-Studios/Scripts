using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coin_Pickup_Script : MonoBehaviour
{
    //public int CoinCount;

    //public GameObject Player;

    //bool foundCoin = false;

    //bool DestroyItem = false;

    private int count;
    public Text countText;

    //public AudioClip pickUpSound;
    //AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Player = GameObject.FindGameObjectWithTag("Player");
        count = 0;
        SetCountText();

        //audioSource.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //CoinCollection();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag ("Coin"))
        {

            //foundCoin = true;

            Debug.Log("Player picked up coin");

            count = count + 1;
            SetCountText();
            Destroy(other.gameObject);

            //audioSource.PlayOneShot(pickUpSound);
        }
       /* else
        {
            foundCoin = false;
        }*/
    }

    /*void CoinCollection()
    {
        if (foundCoin == true)
        {
            //CoinCount += 1;
            Debug.Log(CoinCount);

            foundCoin = false;

            DestroyItem = true;
            DestroyPickup();
        }
        else
        {
            foundCoin = false;
        }
    }

    void DestroyPickup()
    {
        if (DestroyItem == true)
        {
            Destroy(this.gameObject);

            DestroyItem = false;
        }
        else
        {
            DestroyItem = false;
        }
    }*/

    void SetCountText()
    {
        countText.text = "Count: " + count.ToString();
    }
}
