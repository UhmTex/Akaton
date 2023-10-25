using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

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

    private Transform _playerTarget;
    private Transform _chaseStartPos;

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
            LookAt(CurrentWalkPoint);
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
                _playerTarget = interactable.transform;

                isPlayerDetected = true;

                Vector3 interpolatedPos = Vector3.Lerp(transform.position, _playerTarget.position + new Vector3(0, 1, 0), 0.7f);
                Debug.DrawLine(transform.position, interpolatedPos, Color.blue);

                //transform.LookAt(interactable.transform.position + new Vector3(0,1,0));
                LookAt(_playerTarget);
                var step = ChaseSpeed * Time.deltaTime;
                if (Vector3.Distance(transform.position, interpolatedPos) > 1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, interpolatedPos, step);
                }
               
            }
            else
            {
                _playerTarget = null;
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

    private void LookAt(Transform Target)
    {
        Vector3 relativePos = Target.position - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
}