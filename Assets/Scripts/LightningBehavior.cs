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


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Transform spawnPlane = RandomSpawns[Random.Range(0, RandomSpawns.Length)];

            Vector3 spawnPos = GetRandomPos(spawnPlane);

            print(spawnPos);

            Wrapper.transform.position = spawnPos;

            StartCoroutine("LightningFull");
        }
    }

    public Vector3 GetRandomPos(Transform obj)
    {
        Mesh planeMesh = obj.GetComponent<MeshFilter>().mesh;
        Bounds bounds = planeMesh.bounds;

        float minX = obj.position.x - obj.localScale.x * bounds.size.x * 0.5f;
        float minZ = obj.position.z - obj.localScale.z * bounds.size.z * 0.5f;

        Vector3 newVec = new Vector3(Random.Range(minX, -minX), obj.position.y, Random.Range(minZ, -minZ));

        return newVec;
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
