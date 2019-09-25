using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiotoPlayer : MonoBehaviour
{

    public GameObject Player;
    public Transform PlayerTransform;
    public Vector3 PlayerVector3;
    public Transform AudioTransform;
    public Vector3 AudioVector3;
    public GameObject AudioPlayer;


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        AudioPlayer = this.gameObject;
        AudioTransform = AudioPlayer.transform;
        PlayerVector3 = Player.transform.position;
        PlayerTransform = Player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerVector3 = Player.transform.position;
        AudioVector3 = PlayerVector3;
        AudioTransform = PlayerTransform;
    }
}
