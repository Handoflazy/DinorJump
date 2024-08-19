using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioFeedback : MonoBehaviour
{
    public AudioClip clip;
    public AudioSource targetAudioSource;

    [Range(0.0f, 1.0f)]
    public float volume = 1;


    public void PlayClip()
    {
        targetAudioSource.volume = volume;
        targetAudioSource.PlayOneShot(clip);
    }
    public void PlayerSpecificClip(AudioClip clipToPlay = null)
    {
        if(clipToPlay == null)
        {
            clipToPlay = this.clip;
        }
        targetAudioSource.volume = this.volume;
        targetAudioSource.PlayOneShot(clipToPlay);
    }
}
