using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;
    public PlayerController player;
    public Transform playerTransform;

    public bool linking;

    // Enemy Sounds
    public AudioSource enemy;
    public AudioClip[] audios;

    // Enemy Stats
    public float health = 100f;
    public float damage = 25f;
    public float speed = 2f;

    public void Start() {
        animator.SetFloat("Speed", agent.speed);
        agent = GetComponent<NavMeshAgent>();
        playerTransform = player.GetComponent<Transform>();
        StartCoroutine(playAudios());
    }
    public void Update() {
        agent.SetDestination(playerTransform.position);

        float distance = Vector3.Distance(transform.position, playerTransform.position) ;
        if (distance <= agent.stoppingDistance) {
            // Face Player
            Vector3 direction = (playerTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

            // Attack Player

        }
    }
    public void FixedUpdate() {
        if (agent.isOnOffMeshLink) {
            if (!linking) {
                linking = true;
                agent.speed /= 5;
            } else {
                linking = false;
                agent.velocity = Vector3.zero;
                agent.speed *= 5;
            }
        }else {
            agent.speed = speed;
        }
    }
    public IEnumerator playAudios() {
        yield return null;
        foreach (AudioClip clip in audios) {
            enemy.clip = clip;
            enemy.Play();
            while (enemy.isPlaying) {
                yield return null;
            }
            yield return new WaitForSeconds(1);
        }
        StartCoroutine(playAudios());
    }
    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
