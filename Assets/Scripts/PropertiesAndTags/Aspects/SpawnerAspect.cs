using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct SpawnerAspect : IAspect
{
    private readonly RefRW<SpawnerProperties> spawnerProperties;

    public readonly Random rand { get => rand; set => rand = value; }

    public readonly Entity allyToSpawn => spawnerProperties.ValueRO.allyToSpawn;

    public readonly Entity enemyToSpawn => spawnerProperties.ValueRO.enemyToSpawn;

    public readonly float spawnRate => spawnerProperties.ValueRO.spawnrate;

    public float spawnTimer { get => spawnerProperties.ValueRO.spawnTimer; set => spawnerProperties.ValueRW.spawnTimer = value; }

    public bool isTimeToSpawn => spawnTimer <= 0;

    public bool isAllAllySpawned { get => spawnerProperties.ValueRO.isAllAllySpawned; set => spawnerProperties.ValueRW.isAllAllySpawned = value; }

    public BlobAssetReference<SpawnPositionProperty> enemySpawnPositions { get => spawnerProperties.ValueRO.enemyReference; }
    public BlobAssetReference<SpawnPositionProperty> allySpawnPositions { get => spawnerProperties.ValueRO.allyReference; }



    public LocalTransform GetEnemySpawnPosition(int index)
    {
        LocalTransform localTransform = new LocalTransform
        {
            Position = enemySpawnPositions.Value.positions[index].position,
            Rotation = quaternion.identity,
            Scale = 1
        };

        return localTransform;
    }

    public LocalTransform GetAllySpawnPosition(int index)
    {
        LocalTransform localTransform = new LocalTransform
        {
            Position = allySpawnPositions.Value.positions[index].position,
            Rotation = quaternion.identity,
            Scale = 1
        };

        return localTransform;
    }
}
