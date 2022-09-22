using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UniRx.Triggers;
using UnityEngine.Pool;

public class InGameManager : MonoBehaviour
{
    [SerializeField] private CharacterAssets _characterAssets = null;
    [SerializeField] private BulletPool _bulletPool = null;
    [SerializeField] private Transform _playerSpawnTransform = null;

    // Start is called before the first frame update
    void Start()
    {
        DebugEndGame();

        SpawnPlayer();

#if !UNITY_EDITOR
        // カーソルが画面外に行かないようにする
        Cursor.lockState = CursorLockMode.Confined;
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        var playerController = Instantiate(_characterAssets.PlayerParameter.CharacterObject, _playerSpawnTransform.position, Quaternion.identity).GetComponent<PlayerController>();
        playerController.Initialize(_characterAssets.PlayerParameter.BulletAssets, _bulletPool);
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
                Application.Quit();//ゲームプレイ終了
#endif
            });
#endif
    }
}
