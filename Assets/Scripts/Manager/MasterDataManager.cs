using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using MasterData;

public class MasterDataManager : MonoBehaviour
{
    public static MasterDataManager Instance { get; private set; }

    [SerializeField] Player _player;
    public Player Player => _player;

    public bool IsInitialized { get; private set; }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
    }

    private void Start()
    {
        IsInitialized = true;
    }
}
