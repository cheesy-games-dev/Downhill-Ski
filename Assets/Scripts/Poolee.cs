using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Poolee : MonoBehaviour
{
    public static List<Poolee> Poolees = new();
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

    public void Despawn(DespawnType type = DespawnType.Disable)
    {
        Poolees.Remove(this);
        switch (type)
        {
            case 0:
                Destroy(gameObject);
                break;
            case DespawnType.Disable:
                gameObject.SetActive(false);
                break;
        }
    }

    public enum DespawnType : int
    {
        Destroy = 0,
        Disable,
    }
}
