using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using CodeMonkey.Utils;

public partial class UnitMoveOrderSystem : SystemBase {

    protected override void OnStartRunning()
    {
        
    }

    protected override void OnUpdate() {

        if (Input.GetMouseButtonDown(0)) {
            var castleEntity = SystemAPI.GetSingletonEntity<CastleTag>();
            var transforms = SystemAPI.GetComponentLookup<LocalTransform>();

            float3 pos = transforms[castleEntity].Position;

            var positionV3 = new Vector3(pos.x, pos.y, pos.z);

            float cellSize = PathfindingGridSetup.Instance.pathfindingGrid.GetCellSize();

            var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

            PathfindingGridSetup.Instance.pathfindingGrid.GetXY(positionV3 + new Vector3(1, 1) * cellSize * +.5f, out int endX, out int endY);


            ValidateGridPosition(ref endX, ref endY);
            //CMDebug.TextPopupMouse(x + ", " + y);

            Entities.ForEach((Entity entity, DynamicBuffer<PathPosition> pathPositionBuffer, ref LocalTransform translation) => {
                //		        Debug.LogWarning("Add Component!");
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
    }

    private void ValidateGridPosition(ref int x, ref int y) {
        x = math.clamp(x, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetWidth() - 1);
        y = math.clamp(y, 0, PathfindingGridSetup.Instance.pathfindingGrid.GetHeight() - 1);
    }

}
