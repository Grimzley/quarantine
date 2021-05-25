using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour {

    public Button lab;
    public Button asylum;
    public Button swamp;
    public Button theGiant;
    public Button back;

    public void Start() {
        EventSystem sceneEventSystem = FindObjectOfType<EventSystem>();
        if (sceneEventSystem == null)
        {
            GameObject eventSystem = new GameObject("EventSystem");
            eventSystem.AddComponent<EventSystem>();
            eventSystem.AddComponent<StandaloneInputModule>();
        }

        lab = GameObject.Find("LabButton").GetComponent<Button>();
        lab.onClick.AddListener(Level1);

        asylum = GameObject.Find("AsylumButton").GetComponent<Button>();
        asylum.onClick.AddListener(Level2);

        swamp = GameObject.Find("SwampButton").GetComponent<Button>();
        swamp.onClick.AddListener(Level3);

        theGiant = GameObject.Find("TheGiantButton").GetComponent<Button>();
        theGiant.onClick.AddListener(Level4);

        back = GameObject.Find("BackButton").GetComponent<Button>();
        back.onClick.AddListener(Back);
    }
    public void Level1() {
        SceneManager.LoadScene("Level1");
    }
    public void Level2() {
        //SceneManager.LoadScene("Level2");
    }
    public void Level3() {
        //SceneManager.LoadScene("Level3");
    }
    public void Level4() {
        //SceneManager.LoadScene("Level4");
    }
    public void Back() {
        SceneManager.LoadScene("Menu");
    }
}
