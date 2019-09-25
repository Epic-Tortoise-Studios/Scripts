using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
    public float lightHighIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    /*private AlarmLight alarm;
    private Light mainLight;
    private AudioSource panicAudio;
    private AudioSource[] sirens;*/

    void Awake()
    {
        /*alarm = GameObject.FindGameObjectWithTag("alarm").GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag("mainLight").light;
        panicAudio = transform.Find("secondaryMusic").audio;
        GameObject[] sirenGameObjects = GameObject.FindGameObjectWithTag("siren");
        sirens = new AudioSource[sirenGameObjects.Length];

        for(int i = 0; i < sirens.Length; i++)
        {
            sirens[i] = sirenGameObjects[i].audio;
        }*/
    }

    void Update()
    {
        //SwitchAlarms();
        //MusicFading();
    }
/*
    void SwitchAlarms()
    {
        alarms.alarmOn = (position != resetPosition);

        float newIntensity;

        if (position != resetPosition)
        {
            newIntensity = lightHighIntensity;
        }
        else
        {
            newIntensity = lightLowIntensity
        }

        mainlight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < sirens.Length; i++)
        {
                if(position != resetPosition && !sirens[i].isPlaying)
            {
                sirens[i].Play();
            }
            else if (position == resetPosition)
            {
                sirens[i].Stop();
            }
        }
    }

    void MusicFading()
    {
        if(position != resetPosition)
        {
            audio.volume = Mathf.Lerp(AudioBehaviour.volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            audio.volume = Mathf.Lerp(AudioBehaviour.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }*/
}
