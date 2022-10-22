using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Cysharp.Threading.Tasks;
using System;

public class EnemyWaveSpawner : SpawnerBase
{
    [SerializeField] private GameObject _spawnObject = null;
    [SerializeField] private int _waveNum = 0;
    [SerializeField] private bool _isDelete = false;

    private SpawnObject[] _spawnObjects = null;

    public void CreateSpawnObject()
    {
        var objects = this.GetComponentsInChildren<SpawnObject>();
        if(_isDelete)
        {
            for(int i = 0; i < objects.Length; ++i)
            {
                DestroyImmediate(objects[i].gameObject);
            }
        }

        for(int i = 0; i < _waveNum; ++i)
        {
            var obj = Instantiate(_spawnObject, this.transform.position, Quaternion.identity);
            obj.transform.parent = this.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _spawnObjects = this.GetComponentsInChildren<SpawnObject>();
        if(_spawnObjects.Length <= 0)
        {
            Debug.LogError("スポーンオブジェクトが見つかりませんでした。");
            return;
        }

        EnemySpawner().Forget();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override async UniTask EnemySpawner()
    {
        for (int i = 0; i < _spawnObjects.Length; ++i)
        {
            await base.EnemySpawner();

            var obj = _spawnObjects[i];
            await UniTask.Delay(TimeSpan.FromSeconds(obj.SpawnTime));
            obj.Spawn();

            await UniTask.WaitUntil(() => !obj.IsSpawn);
        }

        _isSpawnerEnd = true;
    }
}

[CustomEditor(typeof(EnemyWaveSpawner))]
public class EnemyWaveSpawnerEditor : Editor
{
    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        EnemyWaveSpawner spawner = target as EnemyWaveSpawner;

        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("スポーンオブジェクト追加"))
        {
            spawner.CreateSpawnObject();
        }
    }
}
