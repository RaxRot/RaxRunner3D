using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    
    private AudioSource _audioSource;
    [SerializeField] private AudioClip swipe;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip injure;
    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip coinCollected;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();

        if (Instance==null)
        {
            Instance = this;
        }
    }

    public void PlaySwipe()
    {
        _audioSource.PlayOneShot(swipe);
    }

    public void PlayJump()
    {
        _audioSource.PlayOneShot(jump);
    }

    public void PlayInjure()
    {
        _audioSource.PlayOneShot(injure);
    }

    public void PlayGameOver()
    {
        _audioSource.PlayOneShot(gameOver);
    }

    public void CollectCoin()
    {
        _audioSource.PlayOneShot(coinCollected);
    }
}
