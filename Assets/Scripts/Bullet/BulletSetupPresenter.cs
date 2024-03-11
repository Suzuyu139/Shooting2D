using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSetupPresenter : PresenterBase
{
    [SerializeField] BulletModel _bulletModel;
    [SerializeField] Transform _viewTf;

    private void Start()
    {
        Initialize().Forget();
    }

    async UniTask Initialize()
    {
        await UniTask.WaitUntil(() => _bulletModel.Id > 0);

        var bullet = MasterDataManager.Instance.Bullet.Bullets.Find(x => x.Id == _bulletModel.Id);
        if(bullet == null)
        {
            Debug.LogError($"Bulletマスターで見つけることができませんでした。ID:{_bulletModel.Id}");
            return;
        }

        var prefab = await AddressablesUtility.GetAssetAsync<GameObject>(bullet.Address);
        Instantiate(prefab, Vector3.zero, Quaternion.identity, _viewTf);

        _bulletModel.SetIsSetupInitialized(true);
        IsInitialized = true;
    }
}
