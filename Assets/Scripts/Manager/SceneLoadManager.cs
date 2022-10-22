using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cysharp.Threading.Tasks;

using static SceneConst;

public class SceneLoadManager : MonoBehaviour
{
    private static readonly string _fadeLayerName = "Fade";

    [SerializeField] private Canvas _canvas = null;
    [SerializeField] private CanvasGroup _canvasGroup = null;
    [SerializeField] private float _fadeSpeed = 1.0f;

    public static SceneLoadManager Instance = null;

    public SceneType PrevScene { get; private set; } = SceneType.Title;
    public SceneType CurrentScene { get; private set; } = SceneType.Title;

    public bool IsFade { get; private set; } = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = _fadeLayerName;
        _canvasGroup.alpha = 0.0f;
    }

    public void ChangeScene(SceneType sceneType, bool isFade = true)
    {
        ChangeSceneAsync(sceneType, isFade).Forget();
    }

    private async UniTask ChangeSceneAsync(SceneType sceneType, bool isFade)
    {
        if(_canvas.worldCamera == null)
        {
            _canvas.worldCamera = Camera.main;
            _canvas.sortingLayerName = _fadeLayerName;
        }

        if(isFade)
        {
            await FadeIn();
        }

        PrevScene = CurrentScene;
        CurrentScene = sceneType;
        SceneManager.LoadScene(sceneType.ToString());

        if(isFade)
        {
            await FadeOut();
        }
    }

    private async UniTask FadeIn()
    {
        IsFade = true;
        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
            return _canvasGroup.alpha >= 1.0f;
        });
    }

    private async UniTask FadeOut()
    {
        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
            return _canvasGroup.alpha <= 0.0f;
        });
        IsFade = false;
    }
}
