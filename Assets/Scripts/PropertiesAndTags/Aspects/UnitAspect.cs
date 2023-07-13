using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using Unity.Collections;

public readonly partial struct UnitAspect: IAspect
{
    private readonly RefRO<LocalTransform> transform;

    private readonly RefRW<UnitProperty> property;

    public float3 position => transform.ValueRO.Position;

    public FixedString128Bytes name => property.ValueRO.name;

    public FixedString128Bytes targetName { get => property.ValueRO.targetName; set => property.ValueRW.targetName = value; }
}

