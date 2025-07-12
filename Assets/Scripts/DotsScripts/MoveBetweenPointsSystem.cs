using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateInGroup(typeof(SimulationSystemGroup))]        
[BurstCompile]
public partial struct MoveBetweenPointsSystem : ISystem
{
    public void OnUpdate(ref SystemState state)
    {
        float dt = SystemAPI.Time.DeltaTime;

        foreach (var (t, m) in SystemAPI
                     .Query<RefRW<LocalTransform>, RefRW<MoveBetweenPoints>>())
        {
            ref var move = ref m.ValueRW;

            if (move.pauseTimer > 0f)
            {
                move.pauseTimer -= dt;
                continue;
            }

            float3 target = move.goingToB == 1 ? move.pointB : move.pointA;
            float3 delta  = target - t.ValueRW.Position;
            float  dist   = math.length(delta);

            if (dist < 0.001f)
            {
                t.ValueRW.Position = target;
                move.goingToB     ^= 1;
                move.pauseTimer    = move.pauseDuration;
            }
            else
            {
                t.ValueRW.Position += math.normalize(delta) * move.speed * dt;
            }
        }
    }
}