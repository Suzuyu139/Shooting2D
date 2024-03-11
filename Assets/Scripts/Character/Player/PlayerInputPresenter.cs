using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerInputPresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;

    PlayerModel.InputData _input = new PlayerModel.InputData();

    private void Start()
    {
        IsInitialized = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!_model.IsSetupInitialized)
        {
            return;
        }

        if (context.phase == InputActionPhase.Performed)
        {
            _input.SetMove(context.ReadValue<Vector2>());
        }
        else
        {
            _input.SetMove(Vector2.zero);
        }

        _model.SetInput(_input);
    }

    public void OnCursorPos(InputAction.CallbackContext context)
    {
        if (!_model.IsSetupInitialized)
        {
            return;
        }

        _input.SetCursorPos(context.ReadValue<Vector2>());
        _model.SetInput(_input);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!_model.IsSetupInitialized)
        {
            return;
        }

        var isFire = context.ReadValueAsButton();

        _input.SerIsFire(isFire);
        _model.SetInput(_input);
    }
}
