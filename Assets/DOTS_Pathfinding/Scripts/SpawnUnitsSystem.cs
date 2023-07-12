using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Mathematics;
using static UnityEngine.EventSystems.EventTrigger;


[UpdateAfter(typeof(PathfindingCM))]
public partial class SpawnUnitsSystem : SystemBase {

    private Unity.Mathematics.Random random;
    private int gridWidth;
    private int gridHeight;

    private bool firstUpdate = true;

    protected override void OnUpdate() {
        if (firstUpdate) {
            firstUpdate = false;
            
            random = new Unity.Mathematics.Random(56);

            Grid<GridNode> pathfindingGrid = PathfindingGridSetup.Instance.pathfindingGrid;
            gridWidth = pathfindingGrid.GetWidth();
            gridHeight = pathfindingGrid.GetHeight();

            SpawnUnits(0);
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            SpawnUnits(100);
        }
    }

    

    private void SpawnUnits(int spawnCount) {
        PrefabEntityComponent prefabEntityComponent = SystemAPI.GetSingleton<PrefabEntityComponent>();

        var ecb = new EntityCommandBuffer(Unity.Collections.Allocator.Temp);

        var rand = SystemAPI.GetAspect<RandomizeLocationAspect>(SystemAPI.GetSingletonEntity< PrefabEntityComponent>());

        for (int i = 0; i < spawnCount; i++) {
            var entity = ecb.Instantiate(prefabEntityComponent.prefabEntity);


            var pos = rand.GetRandomLocalTransform();

            ecb.AddComponent(entity, pos);

            //ecb.Playback(EntityManager);
            //Entity spawnedEntity = EntityManager.Instantiate(prefabEntityComponent.prefabEntity);
            //EntityManager.SetComponentData(spawnedEntity, new LocalTransform { Position = new float3(random.NextInt(gridWidth), random.NextInt(gridHeight), 0f) });
            //EntityManager.SetComponentData(spawnedEntity, new LocalTransform { Position = new float3(0, 0, 0) });
        }
        ecb.Playback(EntityManager);
    }

}
