using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float WalkSpeed;
    public float ChaseSpeed = 6f;

    public Transform[] WalkPoints;
    [SerializeField] int _numFound;
    [SerializeField] Transform _interactionPoint;
    [SerializeField] float _interactionPointRadius = 1f;
    [SerializeField] LayerMask _interactableMask;

    private readonly Collider[] _colliders = new Collider[3];

    private Transform CurrentWalkPoint;

    private bool isPlayerDetected = false;


    private void Start()
    {
        CurrentWalkPoint = WalkPoints[0];
    }

    private void Update()
    {       
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        DetectAndAttack();

        if (!isPlayerDetected )
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentWalkPoint.position, WalkSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, CurrentWalkPoint.position) < 1f)
            {
                CurrentWalkPoint = WalkPoints[Random.Range(0, WalkPoints.Length)];
            }
        }

    }

    public void DetectAndAttack()
    {
        if (_numFound > 0)
        {
            var interactable = _colliders[0].gameObject;
            if (interactable.CompareTag("Player"))
            {
               isPlayerDetected = true;
               var step = ChaseSpeed * Time.deltaTime;
               transform.position = Vector3.MoveTowards(transform.position, interactable.transform.position, step);
               
            }
            else
            {
                isPlayerDetected = false;
            }
        }
        else
            isPlayerDetected = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }
    }
}