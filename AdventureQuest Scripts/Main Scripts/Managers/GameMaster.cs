using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    private static GameMaster instance;
   // private SavePrefs savePrefs;
    private GameObject player;
    //public Transform[] checkpointPositions;
    public GameObject startPositionObj;
    public Vector3 initialPlayerPos;
    public Vector3 playerLastPos;
    //private Transform switchCheckpoint;

    public GameObject checkpoint1;
    public GameObject checkpoint2;
    public GameObject checkpoint3;
    //public GameObject checkpoint4;
    //public GameObject checkpoint5;
    //public GameObject checkpoint6;
    //public GameObject checkpoint7;



    //public Vector3 C1;
    //public Vector3 C2;
    //public Vector3 C3;
    //public Vector3 C4;
    //public Vector3 C5;
    //public Vector3 C6;
    //public Vector3 C7;

    void Awake()
    {
        /*GameObject[] checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointPositions = new Transform[checkpoints.Length];
        for(int i = 0; 1 < checkpoints.Length; i++)
        {
            checkpointPositions[i] = checkpoints[i].GetComponent<Transform>();
            
        }*/

        //C1 = checkpoint1.transform.position;
        //C2 = checkpoint2.transform.position;
        //C3 = checkpoint3.transform.position;
        //C4 = checkpoint4.transform.position;
        //C5 = checkpoint5.transform.position;
        //C6 = checkpoint6.transform.position;
        //C7 = checkpoint6.transform.position;

        //savePrefs = GameObject.FindGameObjectWithTag("SaveManager").GetComponent<SavePrefs>();
        player = GameObject.FindGameObjectWithTag("Player");

        if(PlayerPrefs.GetFloat("XPosition") == 0 && PlayerPrefs.GetFloat("YPosition") == 0 && PlayerPrefs.GetFloat("ZPosition") == 0)
        {
            Debug.Log("Fresh Start");
            initialPlayerPos = startPositionObj.transform.position;
        }
        else
        {
            Debug.Log("Loading Previous Position");
            initialPlayerPos = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), PlayerPrefs.GetFloat("ZPosition"));
        }

        //initialPlayerPos = new Vector3(PlayerPrefs.GetFloat("XPosition"), PlayerPrefs.GetFloat("YPosition"), PlayerPrefs.GetFloat("ZPosition"));
        player.transform.position = initialPlayerPos;
        //Debug.Log("GameMaster: " + initialPlayerPos);
        


        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
