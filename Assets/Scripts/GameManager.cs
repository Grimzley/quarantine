using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager _instance;

    public static bool isPaused;
    public static bool isDead;

    // Default Settings
    public static bool isFullscreen = true;
    public static int resolutionIndex = -1;
    public static int qualityIndex = 0;
    public static float volume = -10;
    public static float mouseSensitivity = 1;

    // Singleton Pattern
    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        }else {
            DontDestroyOnLoad(this.gameObject);
            _instance = this;
        }
    }
    public static void Reset() {
        isPaused = false;
        isDead = false;
        Time.timeScale = 1f;
    }
}
