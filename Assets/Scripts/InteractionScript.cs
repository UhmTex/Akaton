using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickupScript : MonoBehaviour
{
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionPointRadius = 6f;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] InteractionPromtUI _interactionDisplay;
    [SerializeField] FadeScript _nextLevelCanvas;
    private bool _eWasPressed = false;
    private float _timerForNextScene = 2;
    private float _timerForFade = 2f;

    int _numFound;

    private readonly Collider[] _colliders = new Collider[3];

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask, QueryTriggerInteraction.Collide);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].gameObject;
            if (interactable.CompareTag("Full Crystal"))
            {
                _interactionDisplay.SetUp("E");
                interactable.GetComponent<InteractableObjectScript>().Interact();
                if (Input.GetKeyDown(KeyCode.E)) 
                    _eWasPressed = true;
                if (_eWasPressed && _timerForNextScene != 0)
                    _timerForNextScene -= Time.deltaTime;
                if (_timerForNextScene <= 0)
                {
                    _nextLevelCanvas.FadeIn();
                    _timerForFade -= Time.deltaTime;
                    if ( _timerForFade <= 0 )
                        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
            } 
        }
        else
        {
            _interactionDisplay.Close();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
