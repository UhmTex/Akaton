using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundMusic : MonoBehaviour
{
    public AudioSource _backgroundMusic;
    public AudioSource _backgroundSounds;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        _backgroundMusic.Play();
        _backgroundSounds.Play();
    }
}
