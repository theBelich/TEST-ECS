using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct WalkAspect : IAspect
{
    public readonly RefRW<LocalTransform> transform;
    public readonly RefRO<EnemyTag> tag;

    public void Walk(float3 position, float deltaTime)
    {
        transform.ValueRW.Position += position * deltaTime;
    }
}
