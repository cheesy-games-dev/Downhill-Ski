using UnityEngine;

public class PlayerControllerInput: PlayerController
{
    public override void InputUpdate()
    {
        if (Input.touchCount > 0) Player.LocalPlayer?.Joystick(Input.GetTouch(0).position.x);
        else Player.LocalPlayer?.Joystick(Input.GetAxis("Horizontal"));
        if (Input.GetButton("Jump"))
        {
            Player.LocalPlayer?.Jump();
        }
        if (Input.GetButton("Music"))
        {
            MusicPlayer.Instance?.ChangeSongs();
        }
        if (Input.GetButton("Mute"))
        {
            MusicPlayer.Instance?.TogglePause(!MusicPlayer.Instance.AudioSource.isPlaying);
        }
    }
}
