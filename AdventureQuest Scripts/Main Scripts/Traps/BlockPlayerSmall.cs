using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPlayerSmall : MonoBehaviour
{
    public GameObject PlayerBlock;
    public Vector3 PlayerBlocklocation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerBlock = GameObject.Find("***LEVEL_DEPENDENCIES***/BoxSpawns/PlayerBlockSmall");
        //PlayerBlock.SetActive(false);
        PlayerBlocklocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Boxes")
        {
            //PlayerBlock.SetActive(true);
            Instantiate(PlayerBlock, PlayerBlocklocation, Quaternion.identity);
        }
    }

}