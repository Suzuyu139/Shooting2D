using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;
    protected BulletAsset[] _bulletAssets = null;

    public void Initialize(BulletAsset[] bulletAssets, BulletPool pool)
    {
        _bulletAssets = bulletAssets;
        _bulletPool = pool;
    }
}
