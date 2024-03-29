using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MasterDataManager : MonoBehaviour
{
    public static MasterDataManager Instance { get; private set; }

    [SerializeField] Player _player;
    public Player Player => _player;

    [SerializeField] Bullet _bullet;
    public Bullet Bullet => _bullet;

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
