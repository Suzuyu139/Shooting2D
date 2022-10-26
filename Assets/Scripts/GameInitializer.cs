using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;

public class GameInitializer : MonoBehaviour
{
    [SerializeField] private GameObject[] _initObjects;
#if DEBUG
    [SerializeField] private GameObject[] _debugInitObjects;
#endif

    private void Awake()
    {
        for (int i = 0; i < _initObjects.Length; i++)
        {
            var obj = Instantiate(_initObjects[i]);
            DontDestroyOnLoad(obj);
        }

#if DEBUG
        for (int i = 0; i < _debugInitObjects.Length; i++)
        {
            var obj = Instantiate(_debugInitObjects[i]);
            DontDestroyOnLoad(obj);
        }
#endif
    }
}
