using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Points : MonoBehaviour {

    public class Upgrade {

        public string name;
        public string text;
        public int cost;
        public Upgrade(string name, string text, int cost) {
            this.name = name;
            this.text = text;
            this.cost = cost;
        }

    }

    public Upgrade[] upgrades = new Upgrade[] {
        new Upgrade("Door1", "Press F to Open Door ", 1000)
    };

    public Transform mainCamera;

    // UI Elements
    public TMP_Text instructionText;
    public TMP_Text pointsText;

    public int points;

    public int pointsPerKill = 150;

    public float range = 3f;

    public void Start() {
        instructionText = GameObject.Find("InstructionText").GetComponentInChildren<TMP_Text>();
        pointsText = GameObject.Find("PointsText").GetComponent<TMP_Text>();

        points = 0;
    }
    public void Update() {
        pointsText.text = points.ToString();

        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) {
            if (hit.collider.tag == "Interactable") {
                Upgrade upgrade = null;
                foreach (Upgrade u in upgrades) {
                    if (hit.collider.name == u.name) {
                        upgrade = u;
                    }
                }
                ShowUpgradeInfo(upgrade);
            }
        }else {
            instructionText.text = "";
        }
    }
    public void ShowUpgradeInfo(Upgrade u) {
        instructionText.text = u.text + "[Cost: " + u.cost + "]";
        if (Input.GetKeyDown(KeyCode.F) && points >= u.cost) {
            switch (u.name) {
                case "Door1":
                    Destroy(GameObject.Find("Door1"));
                    break;
            }
            SpendPoints(u.cost);
        }
    }
    public void SpendPoints(int amount) {
        points -= amount;
    }
    public void EnemyKill() {
        points += pointsPerKill;
    }
}
