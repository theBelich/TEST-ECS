using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Mathematics;

public struct NavigationProperties : IComponentData
{
    public BlobAssetReference<BlobAllies> blobAllies;

    public float3 castlePosition;
}

public struct BlobAllies : IComponentData
{
    public BlobArray<Entity> allyEntities;
}
