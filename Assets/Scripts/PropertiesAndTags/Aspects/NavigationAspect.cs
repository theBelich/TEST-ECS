using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct NavigationAspect : IAspect
{
    private readonly RefRW<NavigationProperties> navigationProperties;

    public BlobAssetReference<BlobAllies> blobAllies
    {
        get => navigationProperties.ValueRO.blobAllies;
        set => navigationProperties.ValueRW.blobAllies = value;
    }

    public float3 castlePosition
    {
        get => navigationProperties.ValueRO.castlePosition;
        set => navigationProperties.ValueRW.castlePosition = value;
    }
}
