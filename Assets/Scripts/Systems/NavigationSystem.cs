using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Entities.UniversalDelegates;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;


[BurstCompile]
[UpdateAfter(typeof(FlowSystem))]
public partial struct NavigationSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        //state.RequireForUpdate<SpawnerProperties>();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {

    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var navigation = SystemAPI.GetSingletonEntity<NavigationProperties>();
        var navigationAspect = SystemAPI.GetAspect<NavigationAspect>(navigation);
    }
}
