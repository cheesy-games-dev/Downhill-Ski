using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Player Player;
    public CameraTarget origin;
    public CameraTarget left;
    public CameraTarget right;
    public float t = 1;
    void Start()
    {
        Player = GetComponentInParent<Player>();
    }
    void LateUpdate()
    {
        float x = Player.Stats.ConstantForce.x;
        bool isOrigin = x < right.threshold && x > left.threshold;
        bool isRight = x >= right.threshold;
        CameraTarget selectedTarget = isOrigin ? origin : (isRight ? right : left);// x >= right.threshold ? right : left;
        if (selectedTarget.transform == null) return;
        transform.localPosition = Vector3.Slerp(transform.localPosition, selectedTarget.transform.localPosition, t * Time.fixedDeltaTime);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, selectedTarget.transform.localRotation, t * Time.fixedDeltaTime);
    }
}

[System.Serializable]
public struct CameraTarget
{
    public Transform transform;
    public float threshold;
    public CameraTarget(Transform transform = null, float threshold = 0)
    {
        this.transform = transform;
        this.threshold = threshold;
    }
}