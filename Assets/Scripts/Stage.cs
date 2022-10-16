using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Stage : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform = null;
    [SerializeField] private List<SpawnerBase> _spawners = new List<SpawnerBase>();

    public Transform PlayerTransform => _playerTransform;

    public bool StageEnd()
    {
        if(_spawners.Count <= 0)
        {
            Debug.LogError("スポナーが一つもありません");
            return false;
        }

        if(_spawners.All(x => x.IsSpawnerEnd) && InGameManager.Instance.Enemies.Count <= 0)
        {
            return true;
        }

        return false;
    }
}
