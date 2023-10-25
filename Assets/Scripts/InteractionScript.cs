using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionPointRadius = 6f;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] InteractionPromtUI _interactionDisplay;

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
