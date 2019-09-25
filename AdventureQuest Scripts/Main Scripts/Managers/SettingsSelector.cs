using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsSelector : MonoBehaviour
{
    public AudioMixer MasterMixer;

    public void SetMasterVolume (float volume)
    {
        MasterMixer.SetFloat("Master", volume);
    }

    public void SetSfxLvl(float sfxLvl)
    {
        MasterMixer.SetFloat("SFX", sfxLvl);
    }

    public void SetMusicLvl (float musicLvl)
    {
        MasterMixer.SetFloat("Music", musicLvl);
    }


}
