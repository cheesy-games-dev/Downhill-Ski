using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player Player;
    private Quaternion rotation;
    public float t = 3;
    public float maxRotation = 20;
    void Start()
    {
        Player = GetComponentInParent<Player>();
    }
    void FixedUpdate()
    {
        rotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(new(0,Mathf.Clamp(Player.Stats.ConstantForce.x * t, -maxRotation, maxRotation))), t * Time.deltaTime);
        transform.localRotation = rotation;
    }
}
