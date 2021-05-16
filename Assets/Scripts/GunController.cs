using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public Camera mainCamera;

    // Gun Animations
    public Animator animator;

    // Gun Sounds
    public AudioSource gun;
    public AudioClip shoot;

    // Gun Effects
    public ParticleSystem flash;

    // Gun Stats
    public float range = 100f;
    public float fireRate = 4f;
    private float timeToFire = 0f;

    public void Start() {
        
    }
    public void Update() {
        if (Input.GetButtonDown("Fire1") && Time.time >= timeToFire) {
            timeToFire = Time.time + 1f / fireRate;
            animator.SetBool("Shoot", true);
            Shoot();
        }else {
            animator.SetBool("Shoot", false);
        }
    }
    public void Shoot() {
        gun.PlayOneShot(shoot);
        flash.Play();
        //RaycastHit hit;
        //if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range)) {
        //
        //}
    }
}
