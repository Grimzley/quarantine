using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    public Spawner spawner;

    // Health Regeneration
    public Image healthIndicator;
    public float timeToRegenerate = 5f;
    public float regenerateTime;
    public bool isRegenerating;

    // Mouse Input
    public Transform mainCamera;
    public float mouseSensitivity = 100f;
    public float xRotation = 0f;

    // Key Input
    public float walkingSpeed = 5f;
    public float runningSpeed;
    public float currentSpeed;
    public float jumpHeight = 0.75f;
    public bool isGrounded;

    public float gravity = -9.81f * 2f;
    public float groundDistance = 0.3f;
    public CharacterController controller;
    public Vector3 move;
    public Vector3 velocity;
    public Transform groundCheck;
    public LayerMask groundMask;

    // Player Sounds
    public AudioSource playerMove;

    // Player Stats
    public float health = 100f;

    // UI Elements
    public GameOver gameOverScreen;

    public void Start() {
        healthIndicator = GameObject.Find("Health").GetComponent<Image>();
        runningSpeed = walkingSpeed * 1.5f;
        currentSpeed = walkingSpeed;
        playerMove.Play();
        playerMove.Pause();
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Update() {
        if (!GameManager.isPaused && !GameManager.isDead) {
            playerMove.UnPause();
            // Mouse Input
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * GameManager.mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * GameManager.mouseSensitivity * Time.deltaTime;

            transform.Rotate(Vector3.up * mouseX);
            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 180 degree view looking up and down
            mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

            // Key Input
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (isGrounded && velocity.y < 0) {
                velocity.y = 0;
            }
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            if (Input.GetButtonDown("Jump") && isGrounded) {
                velocity.y = Mathf.Sqrt(jumpHeight * gravity * -2f); // The velocity required to jump a height h: v = sqrt(h*g*-2)
            }
            if (x == 0 && z == 0 || !isGrounded) {
                playerMove.Pause();
            } else {
                playerMove.UnPause();
            }
            if (Input.GetKeyDown("left shift")) {
                currentSpeed = runningSpeed;
            } else if (Input.GetKeyUp("left shift")) {
                currentSpeed = walkingSpeed;
            }
            move = transform.right * x + transform.forward * z;
            controller.Move(move * currentSpeed * Time.deltaTime);
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);

            // Health Regeneration
            if (health < 100) {
                regenerateTime -= Time.deltaTime;
                if (!isRegenerating) {
                    StartCoroutine(RegenerateHealth());
                }
            }
        }else {
            playerMove.Pause();
        }
    }
    public void TakeDamage(float damage) {
        health -= damage;
        healthIndicator.canvasRenderer.SetAlpha(healthIndicator.canvasRenderer.GetAlpha() + damage);
        regenerateTime = timeToRegenerate;
        if (health <= 0) {
            Die();
        }
    }
    public IEnumerator RegenerateHealth() {
        isRegenerating = true;
        yield return new WaitForSeconds(timeToRegenerate);
        if (regenerateTime <= 0) {
            TakeDamage(-25f);
        }
        isRegenerating = false;
    }
    public void Die() {
        GameManager.isDead = true;
        gameOverScreen.GameOverScreen(spawner.nextRound + 1);
    }
}
