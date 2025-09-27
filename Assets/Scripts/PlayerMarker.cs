using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class PlayerMarker : MonoBehaviour
{
    public static PlayerMarker Current { get; set; }

    void Start()
    {
        Current = this;
        SpawnPlayer();
    }
    public static AsyncOperationHandle<GameObject> PlayerSpawningHandle = new();

    public static async void SpawnPlayer()
    {
        PlayerSpawningHandle = Addressables.InstantiateAsync(GameManager.Instance.Data.MapReferences.PlayerPrefab, Current.transform.position, Current.transform.rotation);
        await PlayerSpawningHandle.Task;
    }
#if UNITY_EDITOR
    /*
        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (!GameManager.GetData().MapReferences.PlayerPrefab.editorAsset) return;
            var filters = GameManager.Instance.Data.MapReferences.PlayerPrefab.editorAsset.GetComponentsInChildren<MeshFilter>();
            foreach (var filter in filters)
            {
                Gizmos.DrawWireMesh(filter.sharedMesh, transform.position, transform.rotation);
            }
        }
        */
#endif
}
