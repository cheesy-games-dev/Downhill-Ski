using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(fileName = "MapSpawnables", menuName = "freakycheesy/MapSpawnables")]
public class MapSpawnables : ScriptableObject
{
    public AssetReferenceGameObject[] Spawnables;
}
