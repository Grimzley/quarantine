using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Credits : MonoBehaviour {

    public float creditsTime = 15f;

    public void Start() {
        StartCoroutine(RollCredits());
    }
    public IEnumerator RollCredits() {
        yield return new WaitForSeconds(creditsTime);
        SceneManager.LoadScene("Menu");
    }
}
