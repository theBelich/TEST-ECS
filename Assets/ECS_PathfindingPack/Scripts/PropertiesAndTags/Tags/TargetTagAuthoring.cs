using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetTagAuthoring : MonoBehaviour
{
    
}

public class TargetTagAuthoringBaker : Baker<TargetTagAuthoring>
{
    public override void Bake(TargetTagAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new TargetTag());
        AddComponent(entity, new UnitProperty()
        {
            name = authoring.name,
        });
    }
}

public struct TargetTag : IComponentData
{

}
