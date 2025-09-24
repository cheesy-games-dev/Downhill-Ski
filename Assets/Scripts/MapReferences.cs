using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapReferences", menuName = "freakycheesy/MapReferences")]
public class MapReferences : ScriptableObject
{
    public SceneAsset HillScene;
    public MapSpawnables spawnables;
    public static MapReferences Current;

    public MapReferences()
    {
        Current = this;
    }
}
