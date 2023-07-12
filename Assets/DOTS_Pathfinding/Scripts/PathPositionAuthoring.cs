using UnityEngine;
using Unity.Entities;

public class PathPositionAuthoring : MonoBehaviour
{  

}

public class PathPositionAuthoringBaker : Baker<PathPositionAuthoring>
{
    public override void Bake(PathPositionAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddBuffer<PathPosition>(entity);
        AddComponent(entity, new NavTag() { entity = entity});
    }
}
