using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugModel
{
    private float _fps = 0.0f;
    private float _fpsTimer = 0.0f;
    
    public float GetFps()
    {
        _fpsTimer += Time.unscaledDeltaTime;
        if (_fpsTimer >= 1.0f)
        {
            _fps = 1.0f / Time.unscaledDeltaTime;
            _fpsTimer = 0.0f;
        }
        return _fps;
    }
}
