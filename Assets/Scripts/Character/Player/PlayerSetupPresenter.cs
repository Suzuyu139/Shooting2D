using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupPresenter : PresenterBase
{
    [SerializeField] PlayerModel _model;
    [SerializeField] Transform _playerViewParent;

    GameObject _playerObj;

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _model.Id > 0);

        var param = MasterDataManager.Instance.Player.Players.Find(x => x.Id == _model.Id);

        _playerObj = await AddressablesUtility.GetAssetAsync<GameObject>(param.Address);
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
