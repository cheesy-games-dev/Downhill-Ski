using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
    void Start()
    {
        Player.LocalPlayer.SetController(this);
    }
    public abstract void InputUpdate();
}
