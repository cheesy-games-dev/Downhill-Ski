using System;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static Action OnStart;
    public static GameManagerData GetData()
    {
        return Instance ? Instance.Data : new();
    }
    public static void AddScore()
    {
        Instance?.Data?.AddScore();
    }
    public GameManagerData Data = new();
    public static GameManager Instance { get; private set; }

    #region life
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        OnStart?.Invoke();
    }
    void Update()
    {
        Data.OnUpdate();
    }

    #endregion
    #region  logic

    public void StartGame() => Data.StartGame();

    public void EndGame() => Data.EndGame();

    #endregion

    public static void PauseGame()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }
}


[Serializable, JsonObject]
public class GameManagerData
{
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
    public int Score { get; internal set; } = 0;

    [JsonIgnore]
    public MapReferences MapReferences;

    public GameState State = GameState.Empty;
    public enum GameState : int
    {
        Empty = 0,
        Running,
        Ending,
    }

    public GameManagerData()
    {
        Score = 0;
        State = GameState.Empty;
    }

    internal void VerifyHighScore()
    {
        HighScore = Score >= HighScore?Score: HighScore;
    }

    public void AddScore()
    {
        if(State != GameState.Running) return;
        Score++;
    }

    internal void OnUpdate()
    {
        VerifyHighScore();
    }

    internal void EndGame()
    {
        State = GameState.Ending;
        VerifyHighScore();
        Addressables.LoadSceneAsync(MapReferences.HillScene);
    }

    internal void StartGame()
    {
        if (State == GameState.Running) return;
        Score = 0;
        State = GameState.Running;
        Player.StartLocalPlayer();
    }
}