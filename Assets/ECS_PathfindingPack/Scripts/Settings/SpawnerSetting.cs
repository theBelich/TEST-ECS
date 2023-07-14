using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_spawnerSetting", menuName = "Game/SpawnerSetting")]
public class SpawnerSetting : ScriptableObject
{
    public List<SpawnPosition> enemyPositions;
    public List<SpawnPosition> allyPositions;

}
