using System.Collections;
using System.Collections.Generic;
using UnityEditor.AddressableAssets;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Cysharp.Threading.Tasks;
using UnityEngine.ResourceManagement.AsyncOperations;

public static class AddressablesUtility
{
    static List<object> _releaseAssets = new();

    public static async UniTask<T> GetAssetAsync<T>(string address)
    {
        AsyncOperationHandle<T> task = default(AsyncOperationHandle<T>);
        task = Addressables.LoadAssetAsync<T>(address);
        await task;

        if (task.Result == null)
        {
            Debug.LogError($"アセットの読み込みに失敗 アドレス：{address}");
            Addressables.Release(task);
            return default(T);
        }

        _releaseAssets.Add(task.Result);
        return task.Result;
    }

    public static void ReleaseAll()
    {
        foreach (var asset in _releaseAssets)
        {
            if (asset != null)
            {
                Addressables.Release(asset);
            }
        }

        _releaseAssets.Clear();
    }

    public static void ReleaseAsset(object asset)
    {
        if (_releaseAssets.Contains(asset))
        {
            if (asset != null)
            {
                Addressables.Release(asset);
            }
            _releaseAssets.Remove(asset);
        }
    }
}
