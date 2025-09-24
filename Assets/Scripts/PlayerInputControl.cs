using UnityEngine;

public class PlayerInputControl : MonoBehaviour
{
    void Update()
    {
        if(Input.touchCount > 0) Player.LocalPlayer?.Joystick(Input.GetTouch(0).position.x);
        else Player.LocalPlayer?.Joystick(Input.GetAxis("Horizontal"));
        if (Input.GetButton("Jump"))
        {
            Player.LocalPlayer?.Jump();
        }
        if (Input.GetButton("Camera"))
        {
            Player.LocalPlayer?.ChangeCamera();
        }
    }
}
