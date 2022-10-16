using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    private bool _isStop = false;
    private float _elapsedTime = 0.0f;
    private float _timer;

    public bool IsStop => _isStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_isStop)
        {
            _elapsedTime += Time.unscaledDeltaTime;
            if(_elapsedTime >= _timer)
            {
                Time.timeScale = 1.0f;
                _isStop = false;
            }
        }
    }

    public void SetTimeScale(float timeScale, float timer)
    {
        _isStop = true;
        Time.timeScale = timeScale;
        _timer = timer;
        _elapsedTime = 0.0f;
    }
}
