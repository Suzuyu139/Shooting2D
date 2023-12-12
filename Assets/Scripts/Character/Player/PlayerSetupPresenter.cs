using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupPresenter : PresenterBase
{
    [SerializeField] Transform _playerViewParent;

    GameObject _playerObj;

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        _playerObj = await AddressablesUtility.GetAssetAsync<GameObject>("Player/Player001/Player001.prefab");
        if (_playerObj == null)
        {
            return;
        }

        Instantiate(_playerObj, _playerViewParent);

        IsInitialized = true;
    }

    private void OnDestroy()
    {
        if (_playerObj != null)
        {
            AddressablesUtility.ReleaseAsset(_playerObj);
        }
    }
}
