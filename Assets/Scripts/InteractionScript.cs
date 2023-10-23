using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionPointRadius = 1f;
    [SerializeField] LayerMask _interactableMask;
    [SerializeField] int _numFound;
    [SerializeField] InteractionPromtUI _interactionDisplay;

    private readonly Collider[] _colliders = new Collider[3];

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);

        if (_numFound > 0)
        {
            var interactable = _colliders[0].gameObject;
            if (interactable.CompareTag("Interactable"))
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
