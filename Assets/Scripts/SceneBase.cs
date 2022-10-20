using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBase : MonoBehaviour
{
    [SerializeField] private Object[] _scenes;

    private void Start()
    {
        for (int i = 0; i < _scenes.Length; i++)
        {
            SceneManager.LoadScene(_scenes[i].name, LoadSceneMode.Additive);
        }
    }
}
