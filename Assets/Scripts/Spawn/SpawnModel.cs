using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnModel : MonoBehaviour
{
    [SerializeField] SpawnType _spawnType = SpawnType.None;
    public SpawnType spawnType => _spawnType;

    [SerializeField] int _spawnId = 0;
    public int SpawnId => _spawnId;

    [SerializeField] GameObject _playerObj;
    public GameObject PlayerObj => _playerObj;
}

public enum SpawnType
{
    None,
    Player,
    Enemy,
}
