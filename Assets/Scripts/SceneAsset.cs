using UnityEngine.AddressableAssets;

[System.Serializable]
public class SceneAsset : AssetReference
{

    public SceneAsset(string guid) : base(guid) { }

    //-----------------------------------------------------------------------------

    public override bool ValidateAsset(string path)
    {
        return path.EndsWith(".unity");
    }
}