using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

    public Button play;
    public Button controls;
    public Button settings;
    public Button credits;
    public Button quit;

    public void Start() {
        play = GameObject.Find("PlayButton").GetComponent<Button>();
        play.onClick.AddListener(Play);

        controls = GameObject.Find("ControlsButton").GetComponent<Button>();
        controls.onClick.AddListener(Controls);

        settings = GameObject.Find("SettingsButton").GetComponent<Button>();
        settings.onClick.AddListener(Settings);

        credits = GameObject.Find("CreditsButton").GetComponent<Button>();
        credits.onClick.AddListener(Credits);

        quit = GameObject.Find("QuitButton").GetComponent<Button>();
        quit.onClick.AddListener(Quit);
    }
    public void Play() {
        SceneManager.LoadScene("LevelSelection");
    }
    public void Controls() {
        SceneManager.LoadScene("Controls");
    }
    public void Settings() {
        SceneManager.LoadScene("Settings");
    }
    public void Credits() {
        SceneManager.LoadScene("Credits");
    }
    public void Quit() {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
}
