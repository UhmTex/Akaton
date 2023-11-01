using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollider : MonoBehaviour
{

    public DeathScript _deathScript;

    private bool hit = false;

    private void OnParticleTrigger()
    {
        if (!hit)
        {
            hit = true;

            _deathScript.PlayDeath();
        }
    }
}
