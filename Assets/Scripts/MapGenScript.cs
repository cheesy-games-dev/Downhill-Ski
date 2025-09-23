using UnityEngine;

public class MapGenScript : MonoBehaviour
{
    public GameObject planeObject;
    public Transform planeSpawn;
    public bool startingPlane = false;
    private bool hasSpawned = false;
    public GameObject[] obstaclesToSpawn;
    public int spawnCount = 0;
    private float distanceToSpawn = 20f;
    private void Start() {
        if (spawnCount <= 0) {
            spawnCount = 0;
            return;
        }
        if (spawnCount > 20) {
            spawnCount = 10;
        }
        for (int i = 0; i < spawnCount; i++) {
            var spawnedObstacle = Instantiate(obstaclesToSpawn[Random.Range(0, obstaclesToSpawn.Length)], transform.position, transform.rotation);
            spawnedObstacle.transform.localPosition += spawnedObstacle.transform.right * Random.Range(-distanceToSpawn, distanceToSpawn) + spawnedObstacle.transform.forward * Random.Range(-distanceToSpawn, distanceToSpawn);
            Destroy(spawnedObstacle, 60);
        }
    }

    private bool Between(int num, int lower, int upper, bool inclusive = false) {
        return inclusive
            ? lower <= num && num <= upper
            : lower < num && num < upper;
    }

    private int increaseSpawnCount() {
        int num = Random.Range(0, 100);
        if (Between(num, 0, 24)) {
            return 1;
        }
        else if (Between(num, 25, 49)) {
            return 2;
        }
        else if (Between(num, 50, 74)) {
            return -1;
        }
        else if (Between(num, 75, 99)) {
            return -2;
        }
        else {
            return 3;
        }
    }

    private void Update() {
        if (!Player.LocalPlayer) {
            hasSpawned = false;
        }
        if (Vector3.Distance(Player.LocalPlayer.transform.position, this.transform.position) < 100f && !hasSpawned) {
            hasSpawned = true;
            MapGenScript spawnedPlane = Instantiate(planeObject, planeSpawn.position, planeSpawn.rotation).GetComponent<MapGenScript>();
            spawnedPlane.spawnCount += increaseSpawnCount();
        }
    }

    private void OnCollisionExit(Collision collision) {
        if (collision.gameObject.GetComponent<SphereCollider>() != null) {
            if (startingPlane)
                return;
            if (!Player.LocalPlayer.alive)
                return;
            Invoke(nameof(DestroyMe), 2.5f);
        }
    }

    public void DestroyMe() {
        if (!Player.LocalPlayer.alive)
            return;
        Destroy(this.gameObject);
    }
}
