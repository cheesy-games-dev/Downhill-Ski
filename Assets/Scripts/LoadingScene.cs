using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LoadingScene : MonoBehaviour
{
    public TMP_Text loadtext;
    public SceneAsset DefaultScene;
    private static string info;
    private void Start()
    {
        var handle = Addressables.LoadAssetsAsync<Object>("", UpdateLoad);
        handle.Completed += FinishedLoading;
    }

    private void FinishedLoading(AsyncOperationHandle<IList<Object>> handle)
    {
        info = "Completed Loading";
        loadtext.text = info;
        Addressables.LoadSceneAsync(DefaultScene);
    }

    private void UpdateLoad(Object obj)
    {
        info = $"Loaded: {obj.name}";
        loadtext.text = info;
    }
}
