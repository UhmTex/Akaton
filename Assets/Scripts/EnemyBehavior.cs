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
    private Vector3 _starterPos;
    private Transform CurrentWalkPoint;

    private bool isPlayerDetected = false;
    private bool aggroRemovedRecently = false;
    private float aggroTimerCount = 3f;
    private float aggroTime = 0f;

    private void Start()
    {
        CurrentWalkPoint = WalkPoints[0];
        _starterPos = Vector3.Lerp(WalkPoints[0].position, WalkPoints[1].position, 0.5f);
    }

    private void Update()
    {
        if (aggroRemovedRecently)
        {
            aggroTime += Time.deltaTime;

            if (aggroTime > aggroTimerCount)
            {
                aggroRemovedRecently = false;
                aggroTime = 0f;
            }
        }

        if (!aggroRemovedRecently) {
            _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactableMask);
        }


        DetectAndAttack();
        DeAggro();

        if (!isPlayerDetected)
        {
            LookAt(CurrentWalkPoint);
            transform.position = Vector3.MoveTowards(transform.position, CurrentWalkPoint.position, WalkSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, CurrentWalkPoint.position) < 1f)
            {
                CurrentWalkPoint = WalkPoints[Random.Range(0, WalkPoints.Length)];
            }
        }
    }

    private void DeAggro()
    {
        if (isPlayerDetected) {
            if (Vector3.Distance(_starterPos, _playerTarget.position) > 20f)
            {
                isPlayerDetected = false;
                aggroRemovedRecently = true;
            }
        }
    }

    private void DetectAndAttack()
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

                LookAt(_playerTarget, 1.5f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player"))
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

    private void LookAt(Transform Target, float BonusHeight)
    {
        Vector3 relativePos = (Target.position + new Vector3(0, BonusHeight, 0)) - transform.position;

        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
    }
}