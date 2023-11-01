using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{

    public DeathScript _deathScript;
    public AudioSource _death_SFX;
    public AudioSource _backgroundMusic;
    public AudioSource _backgroundSounds;

    private bool hit = false;

    private void OnParticleTrigger()
    {
        if (!hit)
        {
            hit = true;
            _death_SFX.Play();
            _backgroundMusic.Stop();
            _backgroundSounds.Stop();
            _deathScript.PlayDeath();
        }
    }
}
