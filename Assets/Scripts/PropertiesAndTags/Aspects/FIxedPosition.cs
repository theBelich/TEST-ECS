using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

public readonly partial struct FixedPosition : IAspect
{
    private readonly RefRO<CastleTag> castle;
}
