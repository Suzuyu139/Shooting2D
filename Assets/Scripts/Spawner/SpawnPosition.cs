using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SpawnPosition : MonoBehaviour
{
    [SerializeField] private int _spawnPositionNum = 0;
    [SerializeField] private bool _isDelete = false;

    public void CreateSpawnPosition()
    {
        var objects = this.GetComponentsInChildren<Transform>();
        if (_isDelete)
        {
            for (int i = 1; i < objects.Length; ++i)
            {
                DestroyImmediate(objects[i].gameObject);
            }
        }

        for (int i = 0; i < _spawnPositionNum; ++i)
        {
            var obj = new GameObject();
            obj.transform.position = Vector3.zero;
            obj.transform.parent = this.transform;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[CustomEditor(typeof(SpawnPosition))]
public class SpawnPositionEditor : Editor
{
    /// <summary>
    /// InspectorのGUIを更新
    /// </summary>
    public override void OnInspectorGUI()
    {
        //元のInspector部分を表示
        base.OnInspectorGUI();

        //targetを変換して対象を取得
        SpawnPosition spawner = target as SpawnPosition;

        //PublicMethodを実行する用のボタン
        if (GUILayout.Button("スポーンポジション追加"))
        {
            spawner.CreateSpawnPosition();
        }
    }
}
