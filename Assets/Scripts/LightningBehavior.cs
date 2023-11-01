using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class LightningBehavior : MonoBehaviour
{
    [SerializeField] private VisualEffect lightningEffect;

    public Transform[] RandomSpawns;

    public GameObject Wrapper;

    public ParticleSystem Lightning_Windup;
    public ParticleSystem Lightning_Explosion;
    public ParticleSystem Lightning_Residue;


    private void Start()
    {
        StartCoroutine("LightningFull");
    }

    IEnumerator LightningFull()
    {
        yield return new WaitForSecondsRealtime(Random.Range(1, 4));

        while (true)
        {
            Wrapper.transform.position = RandomSpawns[Random.Range(0, RandomSpawns.Length)].position;

            Lightning_Windup.Play();

            yield return new WaitForSeconds(5.3f);

            lightningEffect.Play();
            Lightning_Residue.Play();

            Lightning_Explosion.Play();
            Lightning_Windup.Stop();

            yield return new WaitForSeconds(1f);

            Lightning_Explosion.Stop();

            yield return new WaitForSecondsRealtime(1);
        }
    }
}
