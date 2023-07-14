using System.Collections;
using System.Collections.Generic;
using Unity.Entities;

public struct SpawnerProperties : IComponentData
{
    public Entity allyToSpawn;
    public Entity enemyToSpawn;

    public Entity entity;

    public bool isFinishedSpawn;

    public bool isAllAllySpawned;

    public float spawnrate;
    public float spawnTimer;

    public BlobAssetReference<SpawnPositionProperty> enemyReference;
    public BlobAssetReference<SpawnPositionProperty> allyReference;

}
