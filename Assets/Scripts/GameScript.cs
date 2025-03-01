using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    public AudioSource MusicPlayer;
    public Canvas canvas;
    public Toggle musicToggle;
    public TMP_Text highScoreText;
    public PlayerScript playerScript;

    private void Awake() {
        DontDestroyOnLoad(this.gameObject);
        canvas = GetComponent<Canvas>();
        MusicPlayer = GetComponent<AudioSource>();
        playerScript = FindAnyObjectByType<PlayerScript>();
        playerScript.rb.isKinematic = true;
    }

    private Camera GetPlayerCamera() {
        if (playerScript.fpsCamera.enabled) {
            return playerScript.fpsCamera;
        }
        else {
            return playerScript.myCamera;
        }
    }

    private void Update() {
        playerScript = FindAnyObjectByType<PlayerScript>();
        canvas.worldCamera = GetPlayerCamera();
        highScoreText.text = "High Score:\n" + PlayerPrefs.GetInt("HighScore");
        MusicPlayer.enabled = musicToggle.isOn;
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        audioSources[0] = MusicPlayer;
        if (audioSources.Length > 1) {
            Destroy(audioSources[1].gameObject);
        }
    }

    public void StartGame() {
        playerScript.StartGame();
    }

    public void ChangePlayerCamera() {
        playerScript.ChangeCamera();
    }
}
