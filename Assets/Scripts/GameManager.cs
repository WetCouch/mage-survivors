using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private static Vector3 GameBoundaries;

    public static bool isInBoundaries(Vector3 pos) {
         return pos.x < GameBoundaries.x && pos.x > -GameBoundaries.x && pos.z < GameBoundaries.z && pos.z > -GameBoundaries.z;
    }

    private int round = 0;

    [SerializeField] GameObject enemyPrefab;

    void Start() {
        SetBoundaries();
    }

    void Update() {
        if (FindObjectsOfType<EnemyController>().Length == 0) {
            NewRound();
        }
    }

    private void SetBoundaries() {
        GameObject world = GameObject.Find("Ground");

        GameBoundaries = world.transform.position + (world.transform.localScale * 10 / 2);
    }

    private void NewRound() {
        round++;
        SpawnEnemyWave(round);
    }

    private void SpawnEnemyWave(int count) {
        for (int i = 0; i < count; i++) {
            SpawnObject(enemyPrefab);
        }
    }

    private void SpawnObject(GameObject prefab) {
        Instantiate(prefab, GenerateSpawnPos(), prefab.transform.rotation);
    }

    private Vector3 GenerateSpawnPos() {
        float randomCoord(float pos) => Random.Range(-pos, pos);
        
        return new Vector3(randomCoord(GameBoundaries.x), 1, randomCoord(GameBoundaries.z));
    }
}
