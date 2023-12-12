using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool IsInitialized { get; private set; } = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        Instance = this;

        Application.targetFrameRate = 60;

        IsInitialized = true;
    }

    private void OnDestroy()
    {
        AddressablesUtility.ReleaseAll();
    }
}
