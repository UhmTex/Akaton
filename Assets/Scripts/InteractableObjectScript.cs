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
        }
    }
}
