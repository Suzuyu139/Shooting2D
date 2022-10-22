using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.SceneManagement;

public class InGameManager : MonoBehaviour
{
    public static readonly float ScreenLimitPosX = 9.0f;
    public static readonly float ScreenLimitPosY = 5.0f;

    [SerializeField] private TimeManager _timeManager = null;
    [SerializeField] private PoolManager _bulletPool = null;
    [SerializeField] private GameObject _stage = null;

    private List<CharacterControllerBase> _enemies = new List<CharacterControllerBase>();

    public static InGameManager Instance;

    public PlayerController Player { get; private set; }
    public IReadOnlyList<CharacterControllerBase> Enemies => _enemies;
    public TimeManager TimeManager => _timeManager;
    public PoolManager BulletPool => _bulletPool;
    public Stage Stage { get; private set; }
    public bool IsGameOver { get; private set; } = false;
    public bool IsClear { get; private set; } = false;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DebugEndGame();

#if !UNITY_EDITOR
        // カーソルが画面外に行かないようにする
        Cursor.lockState = CursorLockMode.Confined;
#endif

        CreateStage();

        SpawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Time.timeScale = 0.0f;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow))
        {
            Time.timeScale = 1.0f;
        }

        if(Stage.StageEnd() && !Player.IsDeath && !IsClear)
        {
            Debug.Log("StageEnd");
            IsClear = true;
        }

        if(Player == null && !IsGameOver)
        {
            Debug.Log("GameOver");
            IsGameOver = true;
        }
    }

    private void CreateStage()
    {
        Stage = Instantiate(_stage, Vector3.zero, Quaternion.identity).GetComponent<Stage>();
    }

    private void SpawnPlayer()
    {
        var character = MasterManager.Instance.Character;
        Player = Instantiate(character.PlayerParameters.Find(x => x.CharacterId == 1001).CharacterObject, Stage.PlayerTransform.position, Quaternion.identity).GetComponent<PlayerController>();
    }

    public void AddEnemies(CharacterControllerBase enemy)
    {
        _enemies.Add(enemy);
    }

    public void RemoveEnemies(CharacterControllerBase enemy)
    {
        _enemies.Remove(enemy);
    }

    private void DebugEndGame()
    {
#if DEBUG
        // Pを押したらゲーム終了させる
        this.UpdateAsObservable()
            .TakeUntilDestroy(this)
            .Where(_ => Input.GetKeyDown(KeyCode.P))
            .Subscribe(_ =>
            {
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;    //ゲームプレイ終了
#else
                Application.Quit(); //ゲームプレイ終了
#endif
            });
#endif
    }
}
