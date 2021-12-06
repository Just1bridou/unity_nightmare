using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private GameManager m_Game;

    public GameObject AudioPlayer;

    public GameObject soundPosition;

    [Serializable]
    public struct Sound
    {
        public string name;
        public AudioClip audio;
    }
    public Sound[] soundsEffects;

    private Dictionary<string, AudioSource> ActualsSounds = new Dictionary<string, AudioSource>();

    private void Awake()
    {
        m_Game = GameManager.Instance;
    }

    public void playSound(string name, float volume = 0.5f, bool loop = false)
    {
        GameObject instantiateAudio = Instantiate(AudioPlayer);
        AudioSource AudioSource = instantiateAudio.GetComponent<AudioSource>();

        foreach(Sound sound in soundsEffects)
        {
            if(sound.name == name)
            {
                AudioSource.clip = sound.audio;
                AudioSource.volume = volume;
                AudioSource.loop = loop;

                ActualsSounds[name] = AudioSource;

                AudioSource.Play();

                //AudioSource.PlayClipAtPoint(sound.audio);
            }
        }
    }

    public void playShortSound(string name)
    {
        foreach (Sound sound in soundsEffects)
        {
            if (sound.name == name)
            {
                AudioSource.PlayClipAtPoint(sound.audio, soundPosition.transform.position);
            }
        }
    }

    public void stopSound(string name)
    {
        if(ActualsSounds[name] != null)
        {
            ActualsSounds[name].Stop();
        }
    }
}
