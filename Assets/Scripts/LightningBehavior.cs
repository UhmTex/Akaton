using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;

public class LightningBehavior : MonoBehaviour
{
    [SerializeField] private VisualEffect lightningEffect;

    [SerializeField] private Vector3 lightningPos = new Vector3(0,0,0);

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            lightningEffect.SetVector3("SendPos", lightningPos);
        }
    }
}
