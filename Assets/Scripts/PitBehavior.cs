using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitBehavior : MonoBehaviour
{
    [SerializeField] DeathScript _deathScript;
    [SerializeField] AudioSource _death_SFX;
    [SerializeField] AudioSource _backgroundMusic;
    [SerializeField] AudioSource _backgroundSounds;
    [SerializeField] AudioSource _crystal_SFX;
    [SerializeField] LightningBehavior[] _lightnings;
    [SerializeField] EnemyBehavior[] _enemys;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _death_SFX.Play();
            _backgroundMusic.Stop();
            _backgroundSounds.Stop();
            _crystal_SFX.Stop();
            foreach (EnemyBehavior enemy in _enemys)
            {
                enemy._playerIsDead = true;
            }
            foreach (LightningBehavior lightning in _lightnings)
            {
                lightning._playerIsDead = true;
            }
            _deathScript.PlayDeath();
        }
    }
}
