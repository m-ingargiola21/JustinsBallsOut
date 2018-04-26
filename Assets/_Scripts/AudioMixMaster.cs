using UnityEngine;
using System.Collections;
using UnityEngine.Audio;

public class AudioMixMaster : MonoBehaviour
{
    [SerializeField]
    AudioMixer audioMixer;
    [SerializeField]
    GameObject audioMaster;

    public void SetMasterVolume(float masterLevel)
    {
        audioMixer.SetFloat("MasterVol", masterLevel);
    }

    public void SetMusicVolume(float musicLevel)
    {
        audioMixer.SetFloat("MusicVol", musicLevel);
    }

    public void SetSfxVolume(float sfxLevel)
    {
        audioMixer.SetFloat("SfxVol", sfxLevel);
    }

    void Start()
    {
        SetMasterVolume(0f);
        SetMusicVolume(-20f);
        SetSfxVolume(-40f);

        DontDestroyOnLoad(audioMaster);
    }
}
