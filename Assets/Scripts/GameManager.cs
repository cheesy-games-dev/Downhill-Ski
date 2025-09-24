using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region vars
    private const string HighScoreKey = "HighScore";
    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(HighScoreKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(HighScoreKey, value);
        }
    }
    public int Score;
    public static GameManager Instance { get; private set; }
    #endregion

    #region life
    private void Start()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        Player.LocalPlayer.rb.isKinematic = true;
    }

    void Update()
    {

    }

    #endregion
    #region  logic
    public Camera GetPlayerCamera()
    {
        if (Player.LocalPlayer.fpsCamera.enabled)
        {
            return Player.LocalPlayer.fpsCamera;
        }
        else
        {
            return Player.LocalPlayer.myCamera;
        }
    }
    public GameState State = GameState.Empty;
    public enum GameState : int
    {
        Empty = -1,
        Started,
        Dead,
    }
    public void StartGame()
    {
        if (State == 0) return;
        State = GameState.Started;
        Player.LocalPlayer.rb.isKinematic = false;
        Player.LocalPlayer.rb.AddForce(Vector3.forward * 100);
    }

    public void RestartScene()
    {
        if (Score >= HighScore || !PlayerPrefs.HasKey(HighScoreKey))
        {
            HighScore = Mathf.RoundToInt(Score);
        }
        Addressables.LoadSceneAsync(MapReferences.Current.HillScene);
    }

    public void ChangePlayerCamera()
    {
        Player.LocalPlayer.ChangeCamera();
    }
    #endregion
}
