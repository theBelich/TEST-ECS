using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Collections;

public struct UnitProperty : IComponentData
{
    public FixedString128Bytes targetName;
    public FixedString128Bytes name;
}
