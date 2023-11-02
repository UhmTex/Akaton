using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AudioControl : MonoBehaviour
{

    private void Start()
    {
        AudioListener.volume = 0f;

        StartCoroutine(FadeIn());
    }

    IEnumerator FadeOut()
    {
        while (AudioListener.volume > 0)
        {
            AudioListener.volume -= 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }

    IEnumerator FadeIn()
    {
        while (AudioListener.volume < 1)
        {
            AudioListener.volume += 0.1f;

            yield return new WaitForSeconds(0.1f);
        }

        yield return null;
    }
}
