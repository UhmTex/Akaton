using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public class MoveBetweenPointsAuthoring : MonoBehaviour
{
    public Vector3 pointA;
    public Vector3 pointB;
    public float  speed        = 1f;
    public float  pauseDuration = 2f;

    class Baker : Baker<MoveBetweenPointsAuthoring>
    {
        public override void Bake(MoveBetweenPointsAuthoring a)
        {
            var e = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(e, LocalTransform.FromPosition(a.pointA));

            AddComponent(e, new MoveBetweenPoints
            {
                pointA        = a.pointA,
                pointB        = a.pointB,
                speed         = a.speed,
                pauseDuration = a.pauseDuration,
                pauseTimer    = 0,
                goingToB      = 1
            });
        }
    }
}

public struct MoveBetweenPoints : IComponentData
{
    public float3 pointA;
    public float3 pointB;
    public float  speed;
    public float  pauseDuration;
    public float  pauseTimer;
    public byte   goingToB;
}