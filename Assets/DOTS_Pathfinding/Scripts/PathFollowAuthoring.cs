using Unity.Entities;
using UnityEngine;

public struct PathFollow : IComponentData
{
    public int pathIndex;
}

public class PathFollowAuthoring: MonoBehaviour
{
    public int pathIndex;
}

public class PathFollowAuthoringBaker : Baker<PathFollowAuthoring>
{
    public override void Bake(PathFollowAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new PathFollow() { pathIndex = authoring.pathIndex });
    }
}