using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPresenter : PresenterBase
{
    [SerializeField] SpawnModel _model;

    void Start()
    {
        Spawn();

        IsInitialized = true;
    }

    void Spawn()
    {
        if (_model.spawnType == SpawnType.None)
        {
            return;
        }

        if (_model.SpawnId <= 0)
        {
            return;
        }

        switch (_model.spawnType)
        {
            case SpawnType.Player:
                PlayerSpawn();
                break;
            case SpawnType.Enemy:
                break;
            default: 
                return;
        }
    }

    void PlayerSpawn()
    {
        var playerModel = Instantiate(_model.PlayerObj, this.transform.position, Quaternion.identity).GetComponent<PlayerModel>();
        playerModel.SetId(_model.SpawnId);
    }
}
