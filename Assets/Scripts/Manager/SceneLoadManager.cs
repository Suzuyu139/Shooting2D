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

    private bool _isAnimation = false;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }

        _canvas.worldCamera = Camera.main;
        _canvas.sortingLayerName = _fadeLayerName;
        _canvasGroup.alpha = 0.0f;
        _canvas.gameObject.SetActive(false);
    }

    public void ChangeScene(SceneType sceneType, bool isFade = true)
    {
        ChangeSceneAsync(sceneType, isFade).Forget();
    }

    private async UniTask ChangeSceneAsync(SceneType sceneType, bool isFade)
    {
        _canvas.gameObject.SetActive(true);

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

        _canvas.gameObject.SetActive(false);
    }

    private async UniTask FadeIn()
    {
        IsFade = true;
        _isAnimation = true;

        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha += _fadeSpeed * Time.deltaTime;
            return _canvasGroup.alpha >= 1.0f;
        });

        _isAnimation = false;
    }

    private async UniTask FadeOut()
    {
        _isAnimation = true;

        await UniTask.WaitUntil(() =>
        {
            _canvasGroup.alpha -= _fadeSpeed * Time.deltaTime;
            return _canvasGroup.alpha <= 0.0f;
        });

        _isAnimation = false;
        IsFade = false;
    }
}
