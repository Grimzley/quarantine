using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : MonoBehaviour {

    public GunController gun;

    // Knife Animations
    public Animator animator;
    public float knifeTime = 0.667f;

    // Knife Stats
    public float damage = 150f;

    public IEnumerator Knife(float holsterTime) {
        animator.SetTrigger("Knife");
        yield return new WaitForSeconds(knifeTime + holsterTime);
        gun.animator.SetBool("Knife", false);
        yield return new WaitForSeconds(holsterTime);
        gun.isKnifing = false;
    }
    public void OnTriggerEnter(Collider other) {
        if (other.tag == "Enemy") {
            EnemyController enemy = other.GetComponent<EnemyController>();
            enemy.TakeDamage(damage);
        }
    }
}
