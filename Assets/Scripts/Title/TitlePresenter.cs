using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UniRx;
using System;

public class TitlePresenter : MonoBehaviour
{
    [SerializeField] private TitleView _view = null;

    // Start is called before the first frame update
    void Start()
    {
        BindViewEvents();
    }

    private void BindViewEvents()
    {
        _view.OnObservableStartButton
            .TakeUntilDestroy(this)
            .Subscribe(_ => SceneManager.LoadScene("Game"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
