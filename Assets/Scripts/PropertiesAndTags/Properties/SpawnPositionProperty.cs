using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct SpawnPositionProperty : IComponentData
{
    public BlobArray<SpawnPostitionData> positions;
}

public struct SpawnPostitionData: IComponentData
{
    public float3 position;
    public bool isEmpty;
}
