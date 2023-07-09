using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Pathfinding;

public class NavigationMono : MonoBehaviour
{
    public Transform castleTransform;
}


public class NavigationBaker : Baker<NavigationMono>
{
    public override void Bake(NavigationMono authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new NavigationProperties()
        {
            castlePosition = new float3(authoring.castleTransform.position.x, authoring.castleTransform.position.y, 0)
        });
    }
}

public partial class Navigator : SystemBase
{
    IAstarAI ai;

    protected override void OnUpdate()
    {
        var navigationAspect = SystemAPI.GetAspect<NavigationAspect>(SystemAPI.GetSingletonEntity<NavigationProperties>());
        
        foreach (var enemy in SystemAPI.Query<WalkAspect>().WithAll<EnemyTag>())
        {

        }
    }
}