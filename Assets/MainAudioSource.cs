using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class MainAudioSource : MonoBehaviour
{
    public static AudioSource current;
    public static AudioSource current2d;

    void Start()
    {
        current = GetComponent<AudioSource>();
        current2d = current;
        DontDestroyOnLoad(gameObject);
    }

    /// <summary>
    /// Function used to play a short sound, mainly used for UI sounds
    /// </summary>
    /// <param name="clip">the clip to be played</param>
    public static void Play2DSound(AudioClip clip)
    {
        Play2DSound(clip, AudioSettings.Default);
    } 

    public static void Play2DSound(Audio audio)
    {
        Play2DSound(audio.clip, audio.useSettings ? audio.settings : AudioSettings.Default);
    }

    public static void Play2DSound(AudioClip clip,AudioSettings settings)
    {
        if (current2d == null)
        {
            Debug.LogWarning("The current audio source is null!");
            return;
        }
        if (clip == null)
        {
            Debug.LogWarning("The clip you want to play is null");
            return;
        }
        current2d.clip = clip;
        current2d.Play();
    }
}

[Serializable]

public class AudioSettings
{
    [Range(0, 256)]
    public int priority = 128;

    [Range(0, 1)]
    public float volume = 1;

    [Range(-3, 3)]
    public float pitch = 1;

    [Range(-1, 1)]
    public float stereoPan = 0;

    [Range(0, 1)]
    public float spatialBlend = 0;

    [Range(0, 1.1f)]
    public float reverbZoneMix = 1;

    public AudioSettings(int priority, float volume, float pitch, float stereoPan, float spatialBlend, float reverbZoneMix)
    {
        this.priority = priority;
        this.volume = volume;
        this.pitch = pitch;
        this.stereoPan = stereoPan;
        this.spatialBlend = spatialBlend;
        this.reverbZoneMix = reverbZoneMix;
    }

    public static AudioSettings Default => new AudioSettings(128, 1, 1, 0, 0, 1);
}

[Serializable]
public struct Audio
{
    public AudioClip clip;
    public AudioSettings settings;
    public bool useSettings;

    public Audio(AudioClip clip)
    {
        this.clip = clip;
        settings = AudioSettings.Default;
        useSettings = true;
    }
    public Audio(AudioClip clip, AudioSettings settings)
    {
        this.clip = clip;
        this.settings = settings;
        useSettings = true;
    }
    public Audio(AudioClip clip, AudioSettings settings, bool useSettings)
    {
        this.clip = clip;
        this.settings = settings;
        this.useSettings = useSettings;
    }

    public static Audio operator +(Audio audio, AudioClip clip)
    {
        return new Audio(clip, audio.settings, audio.useSettings);
    }
}
