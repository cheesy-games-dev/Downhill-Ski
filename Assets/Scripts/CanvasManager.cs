using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button StartButton;
    public Button ChangeCameraButton;
    public Toggle MusicToggle;
    public TMP_Text HighScoreText;
    public TMP_Text ScoreText;
    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        ChangeCameraButton.onClick.AddListener(ChangePlayerCamera);
        //MusicToggle.onValueChanged.AddListener(GameManager.Instance.);
    }
    void Update()
    {
        HighScoreText.text = $"High Score: \n{GameManager.GetData().HighScore}";
        ScoreText.text = $"Score: \n{GameManager.GetData().Score}";
    }
    public void StartGame() => GameManager.Instance.StartGame();
    public void ChangePlayerCamera() => GameManager.Instance.ChangePlayerCamera();
}
