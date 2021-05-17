using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour {

    public Animator animator;
    public NavMeshAgent agent;
    public PlayerController player;
    public Transform playerTransform;

    // Enemy Stats
    public float health = 100f;

    public void Start() {
        animator.SetFloat("Speed", agent.speed);
        agent = GetComponent<NavMeshAgent>();
        playerTransform = player.GetComponent<Transform>();
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
    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
