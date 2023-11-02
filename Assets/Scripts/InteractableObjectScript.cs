using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private string _prompt;
    private bool _eWasPressed = false;

    public string InteractionPrompt => _prompt;

    public void Interact()
    {
        if (Input.GetKeyUp(KeyCode.E) && !_eWasPressed) 
        {
            CrystalBehavior.Instance.PlayExplosion();
            _eWasPressed=true;
            StartCoroutine(FadeOutAllVolume());
        }
    }

    IEnumerator FadeOutAllVolume()
    {
        while (AudioListener.volume > 0)
        {
            AudioListener.volume -= 0.1f;

            yield return new WaitForSeconds(0.2f);
        }

        yield return null;
    }
}
