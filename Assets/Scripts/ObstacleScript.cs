using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision) {
        if (collision.rigidbody.Equals(Player.LocalPlayer.rb)) {
            Player.LocalPlayer.Die();
        }
    }
}
