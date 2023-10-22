using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    [SerializeField] private string _prompt;
    public string InteractionPrompt => _prompt;

    public void Interact()
    {
        if (Input.GetKeyUp(KeyCode.E)) 
        {
            Debug.Log("Memories consumed");
        }
    }
}
