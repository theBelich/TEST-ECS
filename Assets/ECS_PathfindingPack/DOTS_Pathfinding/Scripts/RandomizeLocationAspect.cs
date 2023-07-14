using System;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public readonly partial struct RandomizeLocationAspect: IAspect
{
    private readonly RefRO<PrefabEntityComponent> position;


    public LocalTransform GetRandomLocalTransform()
    {
        var _transform = new LocalTransform()
        {
            Position = new float3(0, 1, 0)
        };
        return _transform;
    }
}

