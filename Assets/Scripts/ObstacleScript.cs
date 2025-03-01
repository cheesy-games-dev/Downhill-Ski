using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    public PlayerScript playerScript;

    private void Start() {
        playerScript = FindAnyObjectByType<PlayerScript>();
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.Equals(playerScript.rb)) {
            playerScript.Die();
        }
    }
}
