using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using Cysharp.Threading.Tasks;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] private GameInitializer _initializer = null;
    [SerializeField] private TitleView _view = null;

    // Start is called before the first frame update
    void Start()
    {
        Initialize().Forget();
    }

    private async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _initializer.IsInitialized);

        BindViewEvents();
    }

    private void BindViewEvents()
    {
        _view.OnObservableStartButton
            .TakeUntilDestroy(this)
            .Subscribe(_ => SceneLoadManager.Instance.ChangeScene(SceneConst.SceneType.Game));
    }
}
