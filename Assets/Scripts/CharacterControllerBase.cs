using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;

    public void Initialize(BulletPool pool)
    {
        _bulletPool = pool;
    }
}
