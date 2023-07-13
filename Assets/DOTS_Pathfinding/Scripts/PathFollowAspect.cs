using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct PathFollowAspect : IAspect
{
    public readonly RefRW<LocalTransform> translation;
    public readonly RefRW<PathFollow> pathFollow;
    public readonly RefRW<PathfindingParams> pathfindingParams;

    public int index => pathFollow.ValueRO.pathIndex;

    public void ChangePostition(DynamicBuffer<PathPosition> pathPositionBuffer, float deltaTime)
    {
        PathPosition pathPosition = pathPositionBuffer[index];
        float3 targetPosition = new float3(pathPosition.position.x, pathPosition.position.y, 0);
        float3 moveDir = math.normalizesafe(targetPosition - translation.ValueRO.Position);
        float moveSpeed = 3f;

        translation.ValueRW.Position += moveDir * moveSpeed * deltaTime;

        if (math.distance(translation.ValueRW.Position, targetPosition) < .1f)
        {
            // Next waypoint
            pathPositionBuffer.RemoveAt(index);
            pathFollow.ValueRW.pathIndex--;
            pathfindingParams.ValueRW.startPosition = pathPosition.position;
        }
    }
}

