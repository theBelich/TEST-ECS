using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using CodeMonkey.Utils;
using Unity.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine.UIElements;

public partial class UnitMoveOrderSystem : SystemBase
{

    private List<UnitAspect> positions = new List<UnitAspect>();

    protected override void OnUpdate() {

        if (Input.GetMouseButtonDown(0))
        {
            RandomizeMoveDirection();
        }

        if (Input.GetKey(KeyCode.W))
        {
            UpdatePosition();
        }
    }

    private void RandomizeMoveDirection()
    {
        positions.Clear();

        foreach (var item in SystemAPI.Query<UnitAspect>().WithAll<TargetTag>())
        {
            positions.Add(item);
        }

        var castleEntity = SystemAPI.GetSingletonEntity<CastleTag>();
        var transforms = SystemAPI.GetComponentLookup<LocalTransform>();

        float cellSize = PathfindingGridSetup.Instance.pathfindingGrid.GetCellSize();

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        Unity.Mathematics.Random random = new Unity.Mathematics.Random((uint)UnityEngine.Time.frameCount);

        Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref LocalTransform translation, ref UnitProperty unitProperty) =>
        {
            float3 pos;
            UnitAspect targetUnitProperty;
            if (positions.Count > 0)
            {
                targetUnitProperty = positions[random.NextInt(0, positions.Count)];
                pos = targetUnitProperty.position;
                unitProperty.targetName = targetUnitProperty.name;
            }
            else
            {
                pos = transforms[castleEntity].Position;
            }


            var positionV3 = new Vector3(pos.x, pos.y, pos.z);

            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(positionV3 + new Vector3(1, 1) * cellSize * +.5f, out int endX, out int endY);

            ValidateGridPosition(ref endX, ref endY);

            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(translation.Position + new float3(1, 1, 0) * cellSize * +.5f, out int startX, out int startY);

            ValidateGridPosition(ref startX, ref startY);

            ecb.AddComponent(entity, new PathfindingParams
            {
                startPosition = new int2(startX, startY),
                endPosition = new int2(endX, endY)
            });
        }).WithoutBurst().Run();


        ecb.Playback(EntityManager);
    }

    public void MoveTowardPoint(float3 point)
    {
        float cellSize = PathfindingGridSetup.Instance.pathfindingGrid.GetCellSize();

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        float3 pos = point ;

        var positionV3 = new Vector3(pos.x, pos.y, pos.z);

        PathfindingGridSetup.Instance.pathfindingGrid.GetXY(positionV3 + new Vector3(1, 1) * cellSize * +.5f, out int endX, out int endY);

        ValidateGridPosition(ref endX, ref endY);

        Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref LocalTransform translation) =>
        {
            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(translation.Position + new float3(1, 1, 0) * cellSize * +.5f, out int startX, out int startY);

            ValidateGridPosition(ref startX, ref startY);

            ecb.AddComponent(entity, new PathfindingParams
            {
                startPosition = new int2(startX, startY),
                endPosition = new int2(endX, endY)
            });
        }).WithoutBurst().Run();


        ecb.Playback(EntityManager);
    }

    public void UpdatePosition()
    {

        positions.Clear();

        foreach (var item in SystemAPI.Query<UnitAspect>().WithAll<TargetTag>())
        {
            positions.Add(item);
        }

        float cellSize = PathfindingGridSetup.Instance.pathfindingGrid.GetCellSize();

        var ecb = new EntityCommandBuffer(Allocator.Temp);

        Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref LocalTransform translation, ref UnitProperty unitProperty) =>
        {

            float3 pos = GetPosition(unitProperty);

            var positionV3 = new Vector3(pos.x, pos.y, pos.z);


            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(positionV3 + new Vector3(1, 1) * cellSize * +.5f, out int endX, out int endY);

            ValidateGridPosition(ref endX, ref endY);

            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(translation.Position + new float3(1, 1, 0) * cellSize * +.5f, out int startX, out int startY);

            ValidateGridPosition(ref startX, ref startY);

            ecb.AddComponent(entity, new PathfindingParams
            {
                startPosition = new int2(startX, startY),
                endPosition = new int2(endX, endY)
            });
        }).WithoutBurst().Run();


        ecb.Playback(EntityManager);
    }

    private float3 GetPosition(UnitProperty unitProperty)
    {
        Debug.LogWarning(positions.Find(x => x.name == unitProperty.targetName).position);

        return positions.Find(x => x.name == unitProperty.targetName).position;
    }

    private void ValidateGridPosition(ref int x, ref int y) {
        x = math.clamp(x, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetWidth() - 1);
        y = math.clamp(y, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetHeight() - 1);
    }

}
