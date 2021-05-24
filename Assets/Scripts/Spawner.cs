using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [System.Serializable]
    public class Round {

        public EnemyController enemy;
        public int count;
        public float spawnRate;

        public Round(EnemyController enemy, int count, float spawnRate) {
            this.enemy = enemy;
            this.count = count;
            this.spawnRate = spawnRate;
        }
    }
    public enum SpawnState {
        COUNTING,
        SPAWNING,
        WAITING
    }

    public SpawnState state = SpawnState.COUNTING;

    public List<Round> rounds;
    public int nextRound = 0;
    public int numberOfRounds = 1;
    public int[] first10Round = {6, 8, 13, 18, 24, 26, 28, 28, 29, 33};

    public Transform[] spawnPoints;

    public float timeBetweenRounds = 5f;
    public float roundCountdown;

    public float searchCountdown = 1f;

    public void Start() {
        roundCountdown = timeBetweenRounds;
        rounds[0].enemy.health = 150f;
        rounds[0].enemy.agent.speed = 2f;
    }
    public void Update() {
        if (state == SpawnState.WAITING) {
            if (!EnemyIsAlive()) {
                RoundCompleted();
            }
        }else if (roundCountdown <= 0f) {
            if (state != SpawnState.SPAWNING) {
                StartCoroutine(Spawn(rounds[nextRound]));
            }
        }else{
            roundCountdown -= Time.deltaTime;
        }
    }
    public bool EnemyIsAlive () {
        bool temp = true;
        searchCountdown -= Time.deltaTime;
        if (searchCountdown <= 0f) {
            if (GameObject.FindGameObjectWithTag("Enemy") == null) {
                temp = false;
            }
            searchCountdown = 1f;
        }
        return temp;
    }
    public void RoundCompleted() {
        state = SpawnState.COUNTING;
        roundCountdown = timeBetweenRounds;
        nextRound++;
        GenerateNextRound();
    }
    public void GenerateNextRound() {
        Round previousRound = rounds[nextRound - 1];
        EnemyController newEnemy = previousRound.enemy;
        int newCount = previousRound.count;
        float newSpawnRate = previousRound.spawnRate;
        if (nextRound < 9) {
            newEnemy.health += 100;
            newCount = first10Round[nextRound];
        } else {
            if (nextRound == 9){
                newEnemy.agent.speed = 4f;
            }
            newEnemy.health *= 1.1f;
            newCount = EnemiesOnRound(nextRound + 1);
        }
        rounds.Add(new Round(newEnemy, newCount, newSpawnRate));
    }
    /**
     * Formula for calculating the number of zombies per round in solo mode after round 10 according to the Zombies Wiki
     */
    public int EnemiesOnRound(int round) {
        return (int) Mathf.Ceil(0.000058f * Mathf.Pow(round, 3) + 0.074032f * Mathf.Pow(round, 2) + 0.718119f * round + 14.738699f);
    }
    public IEnumerator Spawn(Round round) {
        state = SpawnState.SPAWNING;
        for (int i = 0; i <round.count; i++) {
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(round.enemy.transform, spawnPoint.transform.position, spawnPoint.transform.rotation);
            yield return new WaitForSeconds(1f / (round.spawnRate + Random.Range(-0.1f, 0.1f)));
        }
        state = SpawnState.WAITING;
    }
}
