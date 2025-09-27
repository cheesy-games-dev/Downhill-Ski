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

    public void ChangePlayerCamera()
    {
        Player.LocalPlayer.ChangeCamera();
    }
    #endregion
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
    internal float score = 0;
    public int Score
    {
        get
        {
            return Mathf.RoundToInt(score);
        }
    }
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
        score = 0;
        State = GameState.Empty;
    }

    internal void VerifyHighScore()
    {
        if (Score >= HighScore || !PlayerPrefs.HasKey(HighScoreKey))
        {
            HighScore = Score;
        }
    }

    internal void OnUpdate()
    {
        if (State == GameState.Running)
        {
            score += Time.deltaTime * 1;
        }
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
        State = GameState.Running;
        Player.StartLocalPlayer();
    }
}