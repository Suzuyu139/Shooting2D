using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class PoolModel : MonoBehaviour
{
    [SerializeField] PoolType _type;
    public PoolType Type => _type;

    Dictionary<int, ObjectPool<GameObject>> _poolDictionary = new();
    public Dictionary<int, ObjectPool<GameObject>> PoolDictionary => _poolDictionary;

    [SerializeField] GameObject _prefab = null;
    public GameObject Prefab => _prefab;

    Transform _myTransform = null;

    public void AddPoolDictionary(int id)
    {
        _poolDictionary.Add(id, new ObjectPool<GameObject>(
            createFunc: () =>
            {
                var obj = Instantiate(_prefab);
                obj.transform.parent = this.transform;
                switch (_type)
                {
                    case PoolType.Item:
                        break;

                    case PoolType.Bullet:
                        obj.GetComponent<BulletModel>().SetId(id);
                        break;

                    default:
                        break;
                }
                return obj;
            },
            actionOnGet: target => target.SetActive(true),
            actionOnRelease: target =>
            {
                target.transform.SetParent(_myTransform);
                target.transform.localPosition = Vector3.zero;
                target.transform.localRotation = Quaternion.identity;
                target.SetActive(false);
            },
            actionOnDestroy: target => Destroy(target)));
    }

    public void SetPrefab(GameObject prefab) => _prefab = prefab;

    private void Awake()
    {
        _myTransform = this.transform;
    }
}

public enum PoolType
{
    None,
    Item,
    Bullet,
}