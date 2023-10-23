using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    [SerializeField] private float WalkSpeed;

    public Transform[] WalkPoints;

    private Transform CurrentWalkPoint;

    private bool isPlayerDetected;

    private void Start()
    {
        CurrentWalkPoint = WalkPoints[0];
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, CurrentWalkPoint.TransformPoint(CurrentWalkPoint.position), WalkSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, CurrentWalkPoint.TransformPoint(CurrentWalkPoint.position)) < 1f)
        {
            CurrentWalkPoint = WalkPoints[Random.Range(0, WalkPoints.Length)];
        }
    }
}