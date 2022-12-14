using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using System.Linq;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] private TitleView _view = null;

    private GameQuitDialogPresenter _gameQuitDialogPresenter = null;

    // Start is called before the first frame update
    void Start()
    {
        BindViewEvents();
    }

    private void BindViewEvents()
    {
        _view.OnClickStartButton
            .TakeUntilDestroy(this)
            .Subscribe(_ => SceneLoadManager.Instance.ChangeScene(SceneConst.SceneType.Game));

        _view.OnClickGameQuitButton
            .TakeUntilDestroy(this)
            .Subscribe(_ =>
            {
                if (_gameQuitDialogPresenter == null)
                {
                    var objs = GameObject.FindGameObjectsWithTag(TagName.Dialog).ToList();
                    _gameQuitDialogPresenter = objs.Find(x => x.GetComponent<GameQuitDialogPresenter>())?.GetComponent<GameQuitDialogPresenter>();
                }

                if (_gameQuitDialogPresenter != null)
                {
                    _gameQuitDialogPresenter.Open();
                }
            });
    }
}
