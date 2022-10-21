using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class GameQuitDialogView : DialogViewBase
{
    [SerializeField] private Button _yesButton = null;
    [SerializeField] private Button _noButton = null;

    public IObservable<Unit> OnClickYesButton => _yesButton.OnClickAsObservable();
    public IObservable<Unit> OnClickNoButton => _noButton.OnClickAsObservable();

    public override void Initialize()
    {
        base.Initialize();
        Close();
    }
}
