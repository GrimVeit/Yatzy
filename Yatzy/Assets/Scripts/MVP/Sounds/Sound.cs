using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Sound : ISound
{
    public string ID => id;

    [SerializeField] private string id;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip audioClip;
    [SerializeField] private float volume;
    [SerializeField] private float pitch;
    [SerializeField] private bool isLoop;
    [SerializeField] private bool isPlayAwake;

    private float normalVolume;
    private float durationChangeVolume = 0.2f;

    private bool isMainControl;

    public void Initialize()
    {
        normalVolume = volume;

        audioSource.clip = audioClip;
        audioSource.volume = 0;
        audioSource.pitch = pitch;
        audioSource.loop = isLoop;
        audioSource.playOnAwake = isPlayAwake;

        Coroutines.Start(ChangeVolume(0, normalVolume));

        if (audioSource.playOnAwake)
            audioSource.Play();
    }

    public void Mute()
    {
        audioSource.mute = true;
    }

    public void Unmute()
    {
        audioSource.mute = false;
    }

    public void SetPitch(float pitch)
    {
        audioSource.pitch = pitch;
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }

    public void Play()
    {
        audioSource.Play();
    }

    public void PlayOneShot()
    {
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.PlayOneShot(audioClip);
    }

    public void Stop()
    {
        audioSource.Stop();
    }

    public void Dispose()
    {
        Coroutines.Start(ChangeVolume(normalVolume, 0));
    }

    private IEnumerator ChangeVolume(float startVolume, float endVolume)
    {
        if (audioSource == null) yield break;
        audioSource.volume = startVolume;
        float elapsedTime = 0f;

        while (elapsedTime < durationChangeVolume)
        {
            elapsedTime += Time.deltaTime;
            Debug.Log(Mathf.Lerp(startVolume, endVolume, elapsedTime / durationChangeVolume));
            if (audioSource == null) yield break;
            audioSource.volume = Mathf.Lerp(startVolume, endVolume, elapsedTime/durationChangeVolume);
            yield return null;
        }
    }
}

public interface ISound
{
    public void Play();
    public void PlayOneShot();
    public void Stop();
    public void SetVolume(float vol);
    public void SetPitch(float pitch);
}
