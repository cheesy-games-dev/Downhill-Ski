using System;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region static
    public static Action OnStart;
    #endregion
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
    public MapReferences MapReferences;
    public static GameManager Instance { get; private set; }
    #endregion

    #region life
    private void Start()
    {
        Instance = this;
        MapReferences.Current = MapReferences;
        DontDestroyOnLoad(this.gameObject);
        OnStart?.Invoke();
    }
    void Update()
    {

    }

    #endregion
    #region  logic
    public GameState State = GameState.Empty;
    public enum GameState : int
    {
        Empty = 0,
        Running,
        Ending,
    }
    public void StartGame()
    {
        if (State == GameState.Running) return;
        State = GameState.Running;
        Player.StartLocalPlayer();
    }

    public void EndGame()
    {
        State = GameState.Ending;
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
