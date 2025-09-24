using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Poolee : MonoBehaviour
{
    public static List<Poolee> Poolees = new();
    public AssetReferenceGameObject PrefabRoot;
    public bool OverrideDespawnType = false;
    public DespawnType OverridenDespawnType = DespawnType.Disable;
    void Awake() => Spawn();

    public void Spawn()
    {
        Poolees.Add(this);
        gameObject.SetActive(true);
    }

    public static void DespawnAll(DespawnType type = DespawnType.Destroy)
    {
        foreach (var poolee in Poolees.ToArray()) poolee.Despawn(type);
    }

    private IEnumerator Despawn_Coroutine(DespawnType type, float timer)
    {
        yield return new WaitForSeconds(timer);
        Poolees.Remove(this);
        switch (type)
        {
            case 0:
                if(!Addressables.ReleaseInstance(gameObject)) Destroy(gameObject);
                break;
            case DespawnType.Disable:
                gameObject.SetActive(false);
                break;
        }
    }

    public void Despawn(DespawnType type = DespawnType.Disable, float timer = 0)
    {
        if (OverrideDespawnType) type = OverridenDespawnType;
        StartCoroutine(Despawn_Coroutine(type, timer));
    }

    public enum DespawnType : int
    {
        Destroy = 0,
        Disable,
    }
}
