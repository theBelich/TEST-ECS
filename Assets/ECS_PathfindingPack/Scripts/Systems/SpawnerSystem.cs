//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.Entities;
//using Unity.Transforms;
//using UnityEngine;

//[BurstCompile]
//public partial struct SpawnerSystem : ISystem
//{
//    int i;

//    [BurstCompile]
//    public void OnCreate(ref SystemState state)
//    {
//        state.RequireForUpdate<SpawnerProperties>();
//    }

//    [BurstCompile]
//    public void OnDestroy(ref SystemState state)
//    {

//    }

//    [BurstCompile]
//    public void OnUpdate(ref SystemState state)
//    {
//        var ecb = new EntityCommandBuffer(Allocator.Temp);

//        //var spawner = SystemAPI.GetAspect<SpawnerAspect>(spawnerProperty);
//        var spawner = SystemAPI.GetAspect<SpawnerAspect>(SystemAPI.GetSingletonEntity<SpawnerProperties>());


//        spawner.spawnTimer -= Time.deltaTime;

//        if (!spawner.isTimeToSpawn)
//        {
//            return;
//        }

//        SpawnAlly(spawner, ecb);

//        spawner.spawnTimer = spawner.spawnRate;

//        if (i > spawner.enemySpawnPositions.Value.positions.Length - 1)
//        {
//            i = 0;
//        }

//        if (spawner.enemySpawnPositions.Value.positions[i].isEmpty)
//        {
//            return;
//        }

//        var enemy = ecb.Instantiate(spawner.enemyToSpawn);
//        var pos = spawner.GetEnemySpawnPosition(i);
//        ecb.AddComponent(enemy, new UnitProperty { transform = pos});
//        ecb.AddComponent(enemy, new EnemyTag() { enemy = enemy} );
//        i++;
       

//        ecb.SetComponent(enemy, pos);

//        ecb.Playback(state.EntityManager);

//    }

//    [BurstCompile]
//    private void SpawnAlly(SpawnerAspect spawnerAspect, EntityCommandBuffer ecb)
//    {

//        if (spawnerAspect.isAllAllySpawned)
//        {
//            return;
//        }

//        spawnerAspect.isAllAllySpawned = true;

//        //spawnerAspect.isAllAllySpawned = true;

//        for (int i = 0; i < spawnerAspect.allySpawnPositions.Value.positions.Length; i++)
//        {
//            if (!spawnerAspect.allySpawnPositions.Value.positions[i].isEmpty)
//            {
//                var ally = ecb.Instantiate(spawnerAspect.allyToSpawn);

//                var pos = spawnerAspect.GetAllySpawnPosition(i);

//                ecb.SetComponent(ally, pos);
//            }
//        }
//    }
//}
