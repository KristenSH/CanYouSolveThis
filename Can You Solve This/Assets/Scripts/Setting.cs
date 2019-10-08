using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class Setting : MonoBehaviour
{
    public AudioMixer audioMixer;

    public string back;

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
