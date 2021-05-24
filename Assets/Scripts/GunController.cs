using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Camera mainCamera;
    public PlayerController player;
    public KnifeController knife;

    // Gun Animations
    public Animator animator;
    public bool isRunning;
    public bool isReloading;
    public bool isKnifing;
    public float reloadTime = 2f;
    public float holsterTime = 0.5f;

    // Gun Sounds
    public AudioSource gun;
    public AudioClip shoot;
    public AudioClip reload;

    // Gun Effects
    public ParticleSystem flash;

    // Gun Stats
    public float damage = 20f;
    public float headshotMultiplier = 3f;
    public float fireRate = 4f;
    private float timeToFire = 0f;
    public int maxAmmo = 80;
    public int currentAmmo;
    public int magazineSize = 8;
    public int currentMagazine;

    public void Start() {
        currentAmmo = 32;
        currentMagazine = magazineSize;
    }
    public void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && !isReloading && (player.move.x != 0 || player.move.z != 0)) {
            animator.SetBool("Run", true);
            isRunning = true;
        }else {
            animator.SetBool("Run", false);
            isRunning = false;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire && !isReloading && currentMagazine > 0 && !isRunning && !isKnifing) {
            timeToFire = Time.time + 1f / fireRate;
            animator.SetBool("Shoot", true);
            Shoot();
        }else {
            animator.SetBool("Shoot", false);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadGun();
        }
        if (Input.GetKeyDown(KeyCode.V) && !isKnifing && !isReloading && !isRunning) {
            animator.SetBool("Knife", true);
            isKnifing = true;
            StartCoroutine(knife.Knife(holsterTime));
        }
    }
    public void Shoot() {
        gun.PlayOneShot(shoot);
        flash.Play();
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit)) {
            if (hit.transform.tag == "Enemy") {
                EnemyController enemy = hit.transform.GetComponent<EnemyController>();
                if (hit.collider is CapsuleCollider) {
                    enemy.TakeDamage(damage * headshotMultiplier);
                }
                else{
                    enemy.TakeDamage(damage);
                }
            }
        }
        currentMagazine--;
        if (currentMagazine == 0) {
            ReloadGun();
        }
    }
    public void ReloadGun() {
        if (!isReloading && currentMagazine < magazineSize && currentAmmo > 0 && !isRunning && !isKnifing) {
            StartCoroutine(Reload());
        }
    }
    public IEnumerator Reload() {
        isReloading = true;
        animator.SetTrigger("Reload");
        gun.PlayOneShot(reload);
        yield return new WaitForSeconds(reloadTime);
        int reloadAmount = Mathf.Clamp(magazineSize - currentMagazine, 0, currentAmmo);
        currentMagazine += reloadAmount;
        currentAmmo -= reloadAmount;
        isReloading = false;
    }
}
