using System.Collections;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ObstacleSpawner : MonoBehaviour
{
    public bool SpawnOnStart = true;
    public bool ParentToSpawner = false;
    public AssetReferenceGameObject Obstacle;
    public AsyncOperationHandle<GameObject> SpawnedObstacle = new();
    void Start()
    {
        if (!SpawnOnStart) return;
        SpawnSpawnable();
    }

    public async void SpawnSpawnable()
    {
        SpawnedObstacle = new();
        SpawnedObstacle = Addressables.InstantiateAsync(Obstacle, transform.position, transform.rotation, ParentToSpawner ? transform : null, true);
        await SpawnedObstacle.Task;
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        if (!Obstacle.editorAsset) return;
        var filters = Obstacle.editorAsset.GetComponentsInChildren<MeshFilter>();
        foreach (var filter in filters)
        {
            Gizmos.DrawWireMesh(filter.sharedMesh, transform.position, Quaternion.Euler(transform.eulerAngles + filter.transform.eulerAngles), filter.transform.localScale);
        }
    }
#endif
}
