using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using Unity.Jobs;

public partial class PathFollowSystem : SystemBase {

    private Unity.Mathematics.Random random;

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var pathFollower in SystemAPI.Query<NavTag>())
        {
            var aspect = SystemAPI.GetAspect<PathFollowAspect>(pathFollower.entity);

            if (aspect.index >= 0)
            {
                var buffer = SystemAPI.GetBuffer<PathPosition>(pathFollower.entity);

                aspect.ChangePostition(buffer, deltaTime);
            }
        }

        //Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref LocalTransform translation, ref PathFollow pathFollow) => {
        //    if (pathFollow.pathIndex >= 0)
        //    {
        //        // Has path to follow
        //        PathPosition pathPosition = pathPositionBuffer[pathFollow.pathIndex];
        //        float3 targetPosition = new float3(pathPosition.position.x, pathPosition.position.y, 0);
        //        float3 moveDir = math.normalizesafe(targetPosition - translation.Position);
        //        float moveSpeed = 3f;

        //        translation.Position += moveDir * moveSpeed * deltaTime;

        //        if (math.distance(translation.Position, targetPosition) < .1f)
        //        {
        //            // Next waypoint
        //            pathFollow.pathIndex--;
        //        }
        //    }
        //}).Schedule();
    }

    private void ValidateGridPosition(ref int x, ref int y)
    {
        x = math.clamp(x, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetWidth() - 1);
        y = math.clamp(y, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetHeight() - 1);
    }

}

[UpdateAfter(typeof(PathFollowSystem))]
[DisableAutoCreation]
public partial class PathFollowGetNewPathSystem : SystemBase {
    
    private Unity.Mathematics.Random random;

    private EndSimulationEntityCommandBufferSystem endSimulationEntityCommandBufferSystem;

    protected override void OnCreate() {

        random = new Unity.Mathematics.Random(56);

        endSimulationEntityCommandBufferSystem = new EndSimulationEntityCommandBufferSystem();
    }

    protected override void OnUpdate() {
        int mapWidth = PathfindingGridSetup.Instance.pathfindingGrid.GetWidth();
        int mapHeight = PathfindingGridSetup.Instance.pathfindingGrid.GetHeight();
        float3 originPosition = float3.zero;
        float cellSize = PathfindingGridSetup.Instance.pathfindingGrid.GetCellSize();
        Unity.Mathematics.Random random = new Unity.Mathematics.Random(this.random.NextUInt(1, 10000));
        
        EntityCommandBuffer entityCommandBuffer = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        Entities.WithNone<PathfindingParams>().ForEach((Entity entity, int entityInQueryIndex, in PathFollow pathFollow, in LocalTransform translation) => { 
            if (pathFollow.pathIndex == -1) {

                GetXY(translation.Position + new float3(1, 1, 0) * cellSize * +.5f, translation.Position, cellSize, out int startX, out int startY);

                ValidateGridPosition(ref startX, ref startY, mapWidth, mapHeight);

                int endX = random.NextInt(0, mapWidth);
                int endY = random.NextInt(0, mapHeight);

                entityCommandBuffer.AddComponent(entity, new PathfindingParams { 
                    startPosition = new int2(startX, startY), endPosition = new int2(endX, endY) 
                });
            }
        }).Schedule();

        entityCommandBuffer.Playback(EntityManager);
    }

    private static void ValidateGridPosition(ref int x, ref int y, int width, int height) {
        x = math.clamp(x, 0, width - 1);
        y = math.clamp(y, 0, height - 1);
    }

    private static void GetXY(float3 worldPosition, float3 originPosition, float cellSize, out int x, out int y) {
        x = (int)math.floor((worldPosition - originPosition).x / cellSize);
        y = (int)math.floor((worldPosition - originPosition).y / cellSize);
    }

}
