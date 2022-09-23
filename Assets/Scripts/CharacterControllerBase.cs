using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;
    protected GameObject[] _bulletObjects = null;

    public void Initialize(GameObject[] bulletObjects, BulletPool pool)
    {
        _bulletObjects = bulletObjects;
        _bulletPool = pool;
    }
}
