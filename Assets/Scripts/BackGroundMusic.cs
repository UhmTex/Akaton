using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackGroundMusic : MonoBehaviour
{
    public AudioSource _backgroundMusic;
    public AudioSource _backgroundSounds;
    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex != 4)
            DontDestroyOnLoad(this);
    }

    private void Start()
    {

        if (SceneManager.GetActiveScene().buildIndex != 4)
        {
            _backgroundMusic.Play();
            _backgroundSounds.Play();
        }
    }
}
