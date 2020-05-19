using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.ThirdPersonCharacter;
using Characters.Enemy;

public class GenerateEnemies : MonoBehaviour {

    public GameObject enemy;
    public ThirdPersonCharacterController player;
    public int enemyCount = 5;

    private SphereCollider spawnArea;

    void Start() {
        spawnArea = GetComponent<SphereCollider>();
        enemy.GetComponent<AIEnemy>().tpcc = player;
        StartCoroutine(EnemyDrop());
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
            Instantiate(enemy, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(0.1f);
        }

    }
}
