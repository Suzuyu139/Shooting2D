using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolPresenter : PresenterBase
{
    [SerializeField] PoolModel _model;

    Transform _myTransform;

    private void Start()
    {
        _myTransform = this.transform;

        IsInitialized = true;
    }

    public GameObject Rent(int id, Transform tf = null, Vector3? pos = null, Vector3? localPos = null)
    {
        ObjectPool<GameObject> rentPool = GetPoolOnId(id);

        if (rentPool == null)
        {
            Debug.LogError($"�v�[�����ɂ���ID�F{id}�̃v�[����������܂���ł����B");
            return null;
        }

        var rentObj = rentPool.Get();
        if (rentObj == null)
        {
            Debug.LogError($"����ID�F{id}�̃v�[�����͋�ŃI�u�W�F�N�g���Ăяo���܂���ł����B");
            return null;
        }

        if (tf != null)
        {
            rentObj.transform.SetParent(tf);
            rentObj.transform.localPosition = Vector3.zero;
            rentObj.transform.localRotation = Quaternion.identity;
        }
        if (pos != null)
        {
            rentObj.transform.position = pos.Value;
        }
        if (localPos != null)
        {
            rentObj.transform.localPosition = localPos.Value;
        }

        return rentObj;
    }

    public GameObject Rent(int id, GameObject obj, Transform tf = null, Vector3? pos = null, Vector3? localPos = null)
    {
        _model.SetPrefab(obj);

        ObjectPool<GameObject> rentPool = GetPoolOnId(id);

        if (rentPool == null)
        {
            _model.AddPoolDictionary(id);
            rentPool = GetPoolOnId(id);
        }

        var rentObj = rentPool.Get();
        if (tf != null)
        {
            rentObj.transform.SetParent(tf);
            rentObj.transform.localPosition = Vector3.zero;
            rentObj.transform.localRotation = Quaternion.identity;
        }
        if (pos != null)
        {
            rentObj.transform.position = pos.Value;
        }
        if (localPos != null)
        {
            rentObj.transform.localPosition = localPos.Value;
        }

        return rentObj;
    }

    public void Return(int id, GameObject obj)
    {
        ObjectPool<GameObject> pool;
        pool = GetPoolOnId(id);
        if (pool != null)
        {
            pool.Release(obj);
        }
    }

    ObjectPool<GameObject> GetPoolOnId(int id)
    {
        foreach (var pool in _model.PoolDictionary)
        {
            if (pool.Key == id)
            {
                return pool.Value;
            }
        }

        return null;
    }
}
