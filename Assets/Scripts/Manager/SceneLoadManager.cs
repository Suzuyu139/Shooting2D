using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using static SceneConst;

public class SceneLoadManager : MonoBehaviour
{
    public static SceneLoadManager Instance = null;

    public SceneType PrevScene { get; private set; } = SceneType.Title;
    public SceneType CurrentScene { get; private set; } = SceneType.Title;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void ChangeScene(SceneType sceneType, bool isFade = true)
    {
        PrevScene = CurrentScene;
        CurrentScene = sceneType;
        SceneManager.LoadScene(sceneType.ToString());
    }
}
