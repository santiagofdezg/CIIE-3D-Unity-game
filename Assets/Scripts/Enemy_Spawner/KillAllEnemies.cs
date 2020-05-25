using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KillAllEnemies : MonoBehaviour {

    public bool runAfterBossDeath = true;
    private EnemyHealthSystem[] enemies;

    void OnDestroy() {
        if (runAfterBossDeath) {
            GameHandler.instance.onBossDied -= StartKilling;
            GameHandler.instance.onSpawnerActivated -= LoadCurrentEnemies;
        }
    }

    void Start() {
        if (runAfterBossDeath) {
            GameHandler.instance.onBossDied += StartKilling;
            GameHandler.instance.onSpawnerActivated += LoadCurrentEnemies;
            LoadCurrentEnemies();
        } 
    }

    void LoadCurrentEnemies() {
        enemies = gameObject.GetComponentsInChildren<EnemyHealthSystem>();
        // Debug.Log(enemies.Length);
    }

    void StartKilling() {
        StartCoroutine(KillEnemies());
    }

    IEnumerator KillEnemies() {
        // Kill first the nearest enemies
        Array.Reverse(enemies);
        foreach (var enemy in enemies) {
            if(enemy.enabled) {
                enemy.Kill();
                yield return new WaitForSeconds(0.5f);
            }
        }
        
    }
}
