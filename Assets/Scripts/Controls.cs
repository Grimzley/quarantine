using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Controls : MonoBehaviour {

    public Button back;

    public void Start() {
        back = GameObject.Find("BackButton").GetComponent<Button>();
        back.onClick.AddListener(Back);
    }
    public void Back() {
        SceneManager.LoadScene("Menu");
    }
}
