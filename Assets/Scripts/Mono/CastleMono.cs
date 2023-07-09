using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class CastleMono : MonoBehaviour
{
    
}

public class CastleBaker : Baker<CastleMono>
{
    public override void Bake(CastleMono authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new CastleTag());
    }
}
