using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhadeMusicOutScript : MonoBehaviour
{
    [SerializeField] AudioSource _startMenuMusic;
    private bool _fadeOut = false;

    void Update()
    {
        if (_fadeOut)
        {
            _startMenuMusic.volume -= Time.deltaTime;
        }
    }

    public void FadeOutMusic()
    {
        _fadeOut = true;
    }
}
