using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

    public bool isPaused;

    public Button resume;
    public Button controls;
    public Button settings;
    public Button restart;
    public Button mainMenu;

    public void Start() {
        resume = GameObject.Find("ResumeButton").GetComponent<Button>();
        resume.onClick.AddListener(ResumeGame);

        controls = GameObject.Find("ControlsButton").GetComponent<Button>();
        controls.onClick.AddListener(Controls);

        settings = GameObject.Find("SettingsButton").GetComponent<Button>();
        settings.onClick.AddListener(Settings);

        restart = GameObject.Find("RestartButton").GetComponent<Button>();
        restart.onClick.AddListener(Restart);

        mainMenu = GameObject.Find("MainMenuButton").GetComponent<Button>();
        mainMenu.onClick.AddListener(MainMenu);

        if (GameManager.isPaused){
            PauseGame();
        }else{
            ResumeGame();
        }
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (GameManager.isPaused) {
                ResumeGame();
            }else {
                PauseGame();
            }
        }
    }
    public void PauseGame() {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.Confined;
        GameManager.isPaused = true;
    }
    public void ResumeGame() {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        GameManager.isPaused = false;
    }
    public void Controls() {
        SceneManager.LoadScene("Controls", LoadSceneMode.Additive);
    }
    public void Settings() {
        SceneManager.LoadScene("Settings", LoadSceneMode.Additive);
    }
    public void Restart() {
        GameManager.Reset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        GameManager.Reset();
        SceneManager.LoadScene("Menu");
    }
}
