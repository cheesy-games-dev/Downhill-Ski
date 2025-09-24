using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public Button StartButton;
    public Button ChangeCameraButton;
    public Toggle MusicToggle;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.5f);
        StartButton.onClick.AddListener(GameManager.Instance.StartGame);
        ChangeCameraButton.onClick.AddListener(GameManager.Instance.ChangePlayerCamera);
        //MusicToggle.onValueChanged.AddListener(GameManager.Instance.);
    }
}
