using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneScript : MonoBehaviour
{
    [SerializeField] private AudioSource _craw_SFX;
    private float _timer;
    private bool _timerSeted = false;
    void Update()
    {
        if (!_timerSeted)
        {
            _timer = Random.Range(10, 30);
            _timerSeted = true;
        }

        _timer -= Time.deltaTime;

        if (_timer <= 0)
        {
            _craw_SFX.Play();
            _timerSeted = false;
        }
    }
}
