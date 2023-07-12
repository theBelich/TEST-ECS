using Unity.Entities;
using UnityEngine;

public struct PrefabEntityComponent : IComponentData {

    public Entity prefabEntity;

}

public class PrefabEntityComponentAuthoring : MonoBehaviour
{
    public GameObject prefab;
}

public class PrefabEntityComponentAuthoringBaker : Baker<PrefabEntityComponentAuthoring>
{
    public override void Bake(PrefabEntityComponentAuthoring authoring)
    {
        var entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new PrefabEntityComponent() { prefabEntity = GetEntity(authoring.prefab, TransformUsageFlags.Dynamic) });
    }
}
