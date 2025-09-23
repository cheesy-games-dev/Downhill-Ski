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

    private void Start() {
        DontDestroyOnLoad(this.gameObject);
        canvas = GetComponent<Canvas>();
        MusicPlayer = GetComponent<AudioSource>();
        Player.LocalPlayer.rb.isKinematic = true;
    }

    private Camera GetPlayerCamera() {
        if (Player.LocalPlayer.fpsCamera.enabled) {
            return Player.LocalPlayer.fpsCamera;
        }
        else {
            return Player.LocalPlayer.myCamera;
        }
    }

    private void Update() {
        Player.LocalPlayer = FindAnyObjectByType<Player>();
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
        Player.LocalPlayer.StartGame();
    }

    public void ChangePlayerCamera() {
        Player.LocalPlayer.ChangeCamera();
    }
}
