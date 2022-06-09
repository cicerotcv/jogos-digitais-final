using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AudioManager : MonoBehaviour {
    public Sound[] sounds;
    public static AudioManager instance;

    void Start() {
        Play("BackgroundMusic");
    }

    void Awake() {

        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public Sound FindMusic(string name) {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) {
            Debug.LogWarning("Sound: " + name + " not found!");
            return null;
        }
        return s;
    }

    public void Play(string name) {
        Sound s = FindMusic(name);
        s.source.Play();
    }

    public void Stop(string name) {
        Sound s = FindMusic(name);
        s.source.Stop();
    }

    public void SetVolume(float volume) {
        GameObject audioManager = GameObject.FindWithTag("AudioManager");
        AudioSource[] audioSources = audioManager.GetComponents<AudioSource>();
        foreach (AudioSource audioSource in audioSources) {
            audioSource.volume = volume;
        }
    }
}
