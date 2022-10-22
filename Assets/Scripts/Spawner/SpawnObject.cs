using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using System;

public class SpawnObject : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 0.0f;
    [SerializeField] private GameObject _spawnEnemy = null;
    [SerializeField] private int _spawnNum = 1;
    [SerializeField] private float _spawnWaitTime = 0.0f;
    [SerializeField] private Transform[] _spawnPositions;

    public float SpawnTime => _spawnTime;

    public bool IsSpawn { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        SpawnProcess();
    }

    private void SpawnProcess()
    {
        for (int i = 0; i < _spawnPositions.Length; ++i)
        {
            EnemySpawn(_spawnPositions[i].position).Forget();
        }
    }

    private async UniTask EnemySpawn(Vector3 position)
    {
        IsSpawn = true;
        for (int i = 0; i < _spawnNum; ++i)
        {
            var enemy = Instantiate(_spawnEnemy, position, Quaternion.identity).GetComponent<CharacterControllerBase>();
            InGameManager.Instance.AddEnemies(enemy);
            await UniTask.Delay(TimeSpan.FromSeconds(_spawnWaitTime));
        }
        IsSpawn = false;
    }
}
