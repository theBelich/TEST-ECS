using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Collections;
using Unity.Mathematics;
using System;
using UnityEngine.UIElements;

public class SpawnerMono : MonoBehaviour
{
    public GameObject allyToSpawn;
    public GameObject enemyToSpawn;
    public float spawnrate;
    public List<Transform> enemyPositions;
    public List<Transform> allyPositions;
    public SpawnerSetting spawnPositions;
}

[Serializable]
public class SpawnPosition
{
    public Transform spawnPosition;
    public bool isEmpty;
}

public class SpawnBaker : Baker<SpawnerMono>
{
    public override void Bake(SpawnerMono authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        authoring.spawnPositions.enemyPositions = new List<SpawnPosition>();

        foreach (var pos in authoring.enemyPositions)
        {
            authoring.spawnPositions.enemyPositions.Add(new SpawnPosition() { isEmpty = false, spawnPosition = pos });
        }

        foreach (var pos in authoring.allyPositions)
        {
            authoring.spawnPositions.allyPositions.Add(new SpawnPosition() { isEmpty = false, spawnPosition = pos });
        }

        AddComponent(entity, new SpawnerProperties
        {
            allyToSpawn = GetEntity(authoring.allyToSpawn, TransformUsageFlags.Dynamic),
            enemyToSpawn = GetEntity(authoring.enemyToSpawn, TransformUsageFlags.Dynamic),
            spawnrate = authoring.spawnrate,
            isAllAllySpawned = false,
            isFinishedSpawn = false,
            enemyReference = SetSpawnPointsRef(authoring.spawnPositions.enemyPositions),
            allyReference = SetSpawnPointsRef(authoring.spawnPositions.allyPositions),
            entity = entity
        });
    }


    private BlobAssetReference<SpawnPositionProperty> SetSpawnPointsRef(List<SpawnPosition> coords)
    {
        var builder = new BlobBuilder(Allocator.Temp);
        ref SpawnPositionProperty zombieSpawnPoints = ref builder.ConstructRoot<SpawnPositionProperty>();

        int tombstonesCount = coords.Count;
        BlobBuilderArray<SpawnPostitionData> arrayBuilder = builder.Allocate(ref zombieSpawnPoints.positions, tombstonesCount);

        for (int i = 0; i < coords.Count; i++)
        {
            arrayBuilder[i] = new SpawnPostitionData()
            {
                position = new float3(coords[i].spawnPosition.position.x,
                                         coords[i].spawnPosition.position.y,
                                         coords[i].spawnPosition.position.z),
                isEmpty = coords[i].isEmpty
            };
        }

        var result = builder.CreateBlobAssetReference<SpawnPositionProperty>(Allocator.Persistent);

        return result;
    }
}