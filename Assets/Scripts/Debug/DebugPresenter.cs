using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class DebugPresenter : MonoBehaviour
{
    [SerializeField] private DebugView _view = null;
    private DebugModel _model = null;

    // Start is called before the first frame update
    void Start()
    {
        _view.Initialize();
        _model = new DebugModel();

        BindEvents();
    }

    private void BindEvents()
    {
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                var fps = _model.GetFps();
                _view.SetFps(fps);
            });
    }
}
