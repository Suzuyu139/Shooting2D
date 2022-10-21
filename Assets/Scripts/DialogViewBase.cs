using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.Events;

public class DialogViewBase : MonoBehaviour
{
    private static readonly string _openStateName = "Open";
    private static readonly string _closeStateName = "Close";

    [Header("汎用ダイアログ設定")]
    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private Animator _animator = null;
    [SerializeField] private Button _backScreenButton = null;

    public IObservable<Unit> OnClickBackScreenButton => _backScreenButton.OnClickAsObservable();

    public bool IsOpen { get; private set; } = false;
    public bool IsAnimation { get; private set; } = false;

    public virtual void Initialize()
    {
        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = this.transform.tag;
        _canvas.gameObject.SetActive(false);
    }

    public virtual void Open(bool isAnimation = true)
    {
        if(IsOpen || IsAnimation)
        {
            return;
        }

        _canvas.gameObject.SetActive(true);
        IsOpen = true;
        if(isAnimation && _animator != null)
        {
            _animator.SetBool(nameof(IsOpen), true);
            WaitAnimation(_openStateName).Forget();
        }
    }

    public virtual void Close(bool isAnimation = true)
    {
        if (!IsOpen || IsAnimation)
        {
            return;
        }

        IsOpen = false;
        if(isAnimation && _animator != null)
        {
            _animator.SetBool(nameof(IsOpen), false);
            WaitAnimation(_closeStateName, () => _canvas.gameObject.SetActive(false)).Forget();
        }
        else
        {
            _canvas.gameObject.SetActive(false);
        }
    }

    private async UniTask WaitAnimation(string stateName, UnityAction onCompleted = null)
    {
        IsAnimation = true;

        await UniTask.WaitUntil(() =>
        {
            var state = _animator.GetCurrentAnimatorStateInfo(0);
            return state.IsName(stateName) && state.normalizedTime >= 1;
        });

        IsAnimation = false;

        onCompleted?.Invoke();
    }
}
