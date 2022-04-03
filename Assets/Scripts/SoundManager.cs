using UnityEngine.Audio;
using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    /* 
	 * Based on Brackeys Audio Manager from this tutorial
	 * https://www.youtube.com/watch?v=6OT43pvUyfY
	 * 
	 * First make sure each scene has an audio listener on the main camera
	 * 
	 * 1. Add an audio manager and make it a prefab (empty gameobject with this script attached)
	 * 2. Duplicate the prefab to every scene and edit the master (if you want the sounds to run over several scenes - e.g. the music)
	 * 3. Add each audio clip to the audio manager prefab
	 * 4. Make sure to set the volume and pitch (set the pitch to 1f ideally) for each sound otherwise they won't work
	 * 
	 * When you want to play a sound : SoundManager.instance.Play("SoundNameGoesHere");
	 * use .Stop to stop a specific sound
	 * 
	 * The Mixer Group isn't required, but you can add one to fine tune the sounds if you want.
	 * 
	 */

    public static SoundManager instance;

    public AudioMixerGroup mixerGroup;

    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = mixerGroup;
        }

        Play("Music");
    }
    private Sound GetSound(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
        }
        return s;
    }

    public void Play(string sound)
    {
        Sound s = GetSound(sound);

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Play();
    }

    public void Stop(string sound)
    {
        Sound s = GetSound(sound);
        s.source.Stop();
    }

    public void StopAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.Stop();
        }
    }

}
