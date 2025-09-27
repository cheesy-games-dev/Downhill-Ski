using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button StartButton;
    public Button JumpButton;
    public Button ChangeSongButton;
    public Button PauseButton;
    public Toggle MusicToggle;
    public TMP_Text HighScoreText;
    public TMP_Text ScoreText;
    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        ChangeSongButton.onClick.AddListener(ChangeMusic);
        MusicToggle.onValueChanged.AddListener(ToggleMusic);
        JumpButton.onClick.AddListener(Jump);
        PauseButton.onClick.AddListener(PauseGame);
    }
    void Update()
    {
        HighScoreText.text = $"High Score: \n{GameManager.GetData().HighScore}";
        ScoreText.text = $"Score: \n{GameManager.GetData().Score}";
    }
    public void PauseGame() => GameManager.PauseGame();
    public void StartGame() => GameManager.Instance.StartGame();
    public const int JumpFrameCount = 8;
    public void Jump()
    {
        for (int i = 0; i < JumpFrameCount; i++) Player.LocalPlayer.Jump();
    }
    public void ChangeMusic() => MusicPlayer.Instance.ChangeSongs();
    public void ToggleMusic(bool value) => MusicPlayer.Instance.TogglePause(!value);
}
