using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Camera mainCamera;

    // Gun Animations
    public Animator animator;
    public bool isRunning;
    public bool isReloading;
    public float reloadTime = 2f;

    // Gun Sounds
    public AudioSource gun;
    public AudioClip shoot;
    public AudioClip reload;

    // Gun Effects
    public ParticleSystem flash;

    // Gun Stats
    public float damage = 20f;
    public float force = 50f;
    public float range = 100f;
    public float fireRate = 4f;
    private float timeToFire = 0f;
    public int maxAmmo = 32;
    public int currentAmmo;
    public int magazineSize = 8;
    public int currentMagazine;

    public void Start() {
        currentAmmo = maxAmmo;
        currentMagazine = magazineSize;
    }
    public void Update() {
        if (Input.GetKey(KeyCode.LeftShift) && !isReloading) {
            animator.SetBool("Run", true);
            isRunning = true;
        }else {
            animator.SetBool("Run", false);
            isRunning = false;
        }
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire && !isReloading && currentMagazine > 0 && !isRunning) {
            timeToFire = Time.time + 1f / fireRate;
            animator.SetBool("Shoot", true);
            Shoot();
        }else {
            animator.SetBool("Shoot", false);
        }
        if (Input.GetKeyDown(KeyCode.R)) {
            ReloadGun();
        }
    }
    public void Shoot() {
        gun.PlayOneShot(shoot);
        flash.Play();
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) {
            
            if (hit.rigidbody != null) {
                hit.rigidbody.AddForce(-hit.normal * force);
            }
        }

        currentMagazine--;
        if (currentMagazine == 0) {
            ReloadGun();
        }
    }
    public void ReloadGun() {
        if (!isReloading && currentMagazine < magazineSize && currentAmmo > 0 && !isRunning) {
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
