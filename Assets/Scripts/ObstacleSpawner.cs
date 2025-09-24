using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObstacleSpawner : MonoBehaviour
{
    public bool SpawnOnStart = true;
    public AssetReferenceGameObject Obstacle;
    public AsyncOperationHandle<GameObject> SpawnedObstacle;
    void Start()
    {
        SpawnedObstacle = Addressables.InstantiateAsync(Obstacle);
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!Obstacle.editorAsset) return;
        var renderers = Obstacle.editorAsset.GetComponents<Renderer>();

        foreach (var renderer in renderers)
        {
            var center = renderer.bounds.center;
            var size = renderer.bounds.size;
            Gizmos.DrawCube(center, size);
        }
    }
#endif
}
