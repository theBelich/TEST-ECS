using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;

public struct UnitProperty : IComponentData
{
    public LocalTransform transform;
}
