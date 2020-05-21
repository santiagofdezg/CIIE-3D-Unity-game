﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Characters.ThirdPersonCharacter;
using Characters.Enemy;

public class GenerateEnemies : MonoBehaviour {

    public GameObject[] enemies;
    public ThirdPersonCharacterController player;
    public int enemyCount = 5;

    private SphereCollider spawnArea;

    void Start() {
        spawnArea = GetComponent<SphereCollider>();
        if (enemies == null || enemies.Length == 0) {
            Debug.LogWarning("Add some enemies to the spawner");
        } else {
            foreach (var enemy in enemies) {
                enemy.GetComponent<AIEnemy>().tpcc = player;
            }
            StartCoroutine(EnemyDrop());
        }
        
    }

    Vector3 GetSpawnPosition() {
        Vector3 spawnPosition = new Vector3(Random.insideUnitSphere.x * spawnArea.radius + transform.position.x, 
                        1, Random.insideUnitSphere.z * spawnArea.radius+ transform.position.z);
        spawnPosition.y = Terrain.activeTerrain.SampleHeight(spawnPosition);
   
        return spawnPosition;
    }


    IEnumerator EnemyDrop() {
        for (int i = 0; i < enemyCount; i++) {
            Vector3 spawnPosition = GetSpawnPosition();
            // Debug.Log(spawnPosition);
            int indexEnemy = Random.Range(0,enemies.Length);
            Instantiate(enemies[indexEnemy], spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
