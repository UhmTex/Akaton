using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEndingMusic : MonoBehaviour
{
    [SerializeField] AudioSource _musicSource;
    private float _timer = 3f;
    private bool _isPlaying = false;

    private void Update()
    {
        if (_timer > 0)
            _timer -= Time.deltaTime;

        if(_timer <= 0 && !_isPlaying)
        {
            _musicSource.Play();
            _isPlaying = true;
        }
    }
}
