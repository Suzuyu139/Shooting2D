using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerBase : MonoBehaviour
{
    protected BulletPool _bulletPool = null;
    protected bool _isPaused = false;
    protected CharacterAssets.CharacterParameter _parameter = null;

    public void Initialize(CharacterAssets.CharacterParameter parameter, BulletPool pool)
    {
        _parameter = parameter;
        _bulletPool = pool;
    }
}
