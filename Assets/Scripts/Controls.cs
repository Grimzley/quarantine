using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    public Button back;

    public void Start() {
        EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
        if (sceneEventSystem == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        AudioListener[] audioListeners = FindObjectsOfType<AudioListener>();
        for (int i = 1; i < audioListeners.Length; i++) {
            DestroyImmediate(audioListeners[i]);
        }

        back = GameObject.Find("BackButton").GetComponent<Button>();
        back.onClick.AddListener(Back);
    }
    public void Back() {
        if (SceneManager.sceneCount > 1) {
            SceneManager.UnloadSceneAsync("Controls");
        }else {
            SceneManager.LoadScene("Menu");
        }
    }
}
