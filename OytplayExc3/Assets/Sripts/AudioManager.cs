using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioClip _audioWinClip;
    public AudioClip _audioLoseClip;

    private void Start()
    {
        if (!_audioSource)
            _audioSource.GetComponent<AudioSource>();
    }

    public void PlayWinSound()
    {
        if (!_audioWinClip)
            _audioSource.PlayOneShot(_audioWinClip);
    }

    public void PlayLoseSound()
    {
        if (!_audioLoseClip)
            _audioSource.PlayOneShot(_audioLoseClip);
    }
}
