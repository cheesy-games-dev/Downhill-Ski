using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Poolee), editorForChildClasses: true), CanEditMultipleObjects]
public class PooleeToSpawner : Editor
{
    public const string DeletePrefabKey = "DeletePrefab";
    public static bool DeletePrefab
    {
        get
        {
            return EditorPrefs.GetBool(DeletePrefabKey);
        }
        set
        {
            EditorPrefs.SetBool(DeletePrefabKey, value);
        }
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var prefabs = targets.Cast<Poolee>();
        DeletePrefab = GUILayout.Toggle(DeletePrefab, "Delete Prefab after conversion");
        if (GUILayout.Button("Turn into Spawner"))
        {
            SpawnPrefabs(prefabs);
        }
    }

    public static void SpawnPrefabs(IEnumerable<Poolee> prefabs)
    {
        foreach (var prefab in prefabs.ToList())
        {
            var spawner = new GameObject($"Spawner ({prefab.name})").AddComponent<ObstacleSpawner>();
            spawner.Obstacle = prefab.PrefabRoot;
            spawner.transform.parent = prefab.transform.parent;
            spawner.transform.position = prefab.transform.position;
            spawner.transform.rotation = prefab.transform.rotation;
            DestroyImmediate(prefab.gameObject);
        }
    }
}
