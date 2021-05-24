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

    public float timeBetweenAttacks = 1f;
    public float timeToAttack;
    public float timeToDie = 3f;

    // Enemy Sounds
    public AudioSource enemy;
    public AudioClip[] audios;
    public float audioDelay = 3f;

    // Enemy Stats
    public float health = 150f;
    public float damage = 25f;
    public float speed = 2f;

    public void Start() {
        animator.GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        playerTransform = player.GetComponent<Transform>();
        enemy = GetComponent<AudioSource>();

        animator.SetFloat("Speed", agent.speed);
        timeToAttack = timeBetweenAttacks;
        StartCoroutine(playAudios());
    }
    public void Update() {
        if (agent.enabled) {
            agent.SetDestination(playerTransform.position);
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= agent.stoppingDistance) {
                // Face Player
                Vector3 direction = (playerTransform.position - transform.position).normalized;
                Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

                // Attack Player
                animator.SetBool("Attacking", true);
                timeToAttack -= Time.deltaTime;
                if (timeToAttack <= 0f) {
                    player.TakeDamage(damage);
                    timeToAttack = timeBetweenAttacks;
                }
            } else {
                animator.SetBool("Attacking", false);
                timeToAttack = timeBetweenAttacks;
            }
        }
    }
    //public void FixedUpdate() {
    //    if (agent.isOnOffMeshLink) {
    //        if (!linking) {
    //            linking = true;
    //            agent.speed /= 5;
    //        } else {
    //            linking = false;
    //            agent.velocity = Vector3.zero;
    //            agent.speed *= 5;
    //        }
    //    }else {
    //        agent.speed = speed;
    //    }
    //}
    public IEnumerator playAudios() {
        yield return null;
        AudioClip clip = audios[Random.Range(0, audios.Length)];
        enemy.clip = clip;
        enemy.Play();
        while (enemy.isPlaying) {
            yield return null;
        }
        yield return new WaitForSeconds(audioDelay);
        StartCoroutine(playAudios());
    }
    public void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0) {
            StartCoroutine(Die());
        }
    }
    public IEnumerator Die(){
        animator.SetTrigger("Death");
        agent.enabled = false;
        GetComponent<Rigidbody>().detectCollisions = false;
        yield return new WaitForSeconds(timeToDie);
        Destroy(gameObject);
    }
}
