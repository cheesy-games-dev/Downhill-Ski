namespace UnityEngine.AddressableAssets
{
    public class SceneAsset : AssetReference
    {
        public override bool ValidateAsset(string path)
        {
            return path.ToLower().Contains(".unity");
        }
    }
}