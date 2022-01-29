using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {
    public AudioSource myAS;
    public AudioMixer audioMixer;

    private void Awake() {
        myAS = GetComponent<AudioSource>();
    }

    private void Start() {
        if (!myAS.isPlaying) {
            myAS.time = 36;
            myAS.Play();

        }
    }

    public void setVolume(float newVolume) {
        Debug.Log("Valor: " + newVolume);
        //myAS.volume = newVolume;
        audioMixer.SetFloat("volume", newVolume);
    }
}
