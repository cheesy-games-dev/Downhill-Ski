using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button StartButton;
    public Button ChangeCameraButton;
    public Toggle MusicToggle;

    void Start()
    {
        StartButton.onClick.AddListener(StartGame);
        ChangeCameraButton.onClick.AddListener(ChangePlayerCamera);
        //MusicToggle.onValueChanged.AddListener(GameManager.Instance.);
    }
    public void StartGame() => GameManager.Instance.StartGame();
    public void ChangePlayerCamera() => GameManager.Instance.ChangePlayerCamera();
}
