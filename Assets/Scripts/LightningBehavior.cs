using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class LightningBehavior : MonoBehaviour
{
    [SerializeField] private VisualEffect lightningEffect;

    public GameObject Wrapper;

    public ParticleSystem Lightning_Windup;
    public ParticleSystem Lightning_Explosion;
    public ParticleSystem Lightning_Residue;

    public ParticleSystem test;

    private Vector3 lightningPos;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            StartCoroutine("LightningFull");
        }

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            test.Play();
        }
    }

    IEnumerator LightningFull()
    {
        var randomVector3 = new Vector3(Random.Range(-5, 5), 1, Random.Range(-5, 5));

        Wrapper.SetActive(true);

        Wrapper.transform.position = randomVector3;

        Lightning_Windup.Play();

        yield return new WaitForSeconds(5.3f);

        lightningEffect.SetVector3("SendPos", randomVector3);
        lightningEffect.Play();

        Lightning_Explosion.Play();
        Lightning_Windup.Stop();

        yield return new WaitForSeconds(1f);

        Lightning_Explosion.Stop();

        yield return new WaitForSeconds(1f);

        Lightning_Residue.Play();

        Wrapper.SetActive(false);
    }
}
