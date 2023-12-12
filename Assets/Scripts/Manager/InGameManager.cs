using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameManager : MonoBehaviour
{
    public static InGameManager Instance { get; private set; }

    public bool IsInitialized { get; private set; } = false;

    private void Start()
    {
        Instance = this;

        Cursor.lockState = CursorLockMode.Confined;

        IsInitialized = true;
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
