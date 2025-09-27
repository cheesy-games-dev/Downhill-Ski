using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ObstacleSpawner))]
public class Randomiser : MonoBehaviour
{
    private ObstacleSpawner spawner;
    public BoxCollider boundaries;
    void Awake()
    {
        TryGetComponent(out spawner);
        spawner.SpawnOnStart = false;
    }
    private void Start()
    {
        if (!GameManager.Instance) return;
        var spawnables = GameManager.Instance.Data.MapReferences.spawnables.Spawnables;
        spawner.Obstacle = spawnables[Random.Range(0+Random.Range(0, (spawnables.Length - 1)/2), spawnables.Length - 1)];
        for (int i = 0; i < RNG(); i++)
        {
            SpawnLogic(new());    
        }
    }

    public void SpawnLogic(List<Vector3> registered)
    {
        var x = Random.Range(boundaries.bounds.min.x, boundaries.bounds.max.x);
        var z = Random.Range(boundaries.bounds.min.z, boundaries.bounds.max.z);
        Vector3 pos = new(x, 0, z);
        if (registered.Contains(pos))
        {
            SpawnLogic(registered);
        }
        else
        {
            registered.Add(pos);
            Spawn(pos);
        }
    }

    private int RNG()
    {
        int i = Random.Range(0, 3);
        int x = 6;
        x = Random.Range(2*i, 6*i);
        return x;
    }

    private void Spawn(Vector3 pos)
    {

        spawner.transform.localPosition = pos;
        spawner.SpawnSpawnable();
    }
}
