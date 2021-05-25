using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float creditsTime = 15f;

    public void Start() {
        EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
        if (sceneEventSystem == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        StartCoroutine(RollCredits());
    }
    public IEnumerator RollCredits() {
        yield return new WaitForSeconds(creditsTime);
        SceneManager.LoadScene("Menu");
    }
}
