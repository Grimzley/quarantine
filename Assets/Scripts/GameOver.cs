using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour {

    public GameObject gameOverScreen;

    public TMP_Text roundText;

    public Button tryAgain;
    public Button mainMenu;

    public void Start() {
        roundText = GameObject.Find("RoundNumber").GetComponent<TMP_Text>();

        tryAgain = GameObject.Find("TryAgainButton").GetComponent<Button>();
        tryAgain.onClick.AddListener(TryAgain);

        mainMenu = GameObject.Find("MainMenuButton").GetComponent<Button>();
        mainMenu.onClick.AddListener(MainMenu);

        gameOverScreen.SetActive(false);
    }
    public void DeathRound(int round) {
        if (round == 1) {
            roundText.text = "You Survived 1 Round";
        }else {
            roundText.text = "You Survived " + round + " Rounds";
        }
    }
    public void TryAgain() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void MainMenu() {
        SceneManager.LoadScene("Menu");
    }
}
