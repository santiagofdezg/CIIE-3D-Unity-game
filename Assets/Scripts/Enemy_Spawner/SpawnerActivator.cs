
using UnityEngine;
using Characters.ThirdPersonCharacter;

public class SpawnerActivator : MonoBehaviour {

    public GenerateEnemies[] enemySpawners;
    private ThirdPersonCharacterController player;
    private SphereCollider detectionArea;

    void Start() {
        player = ThirdPersonCharacterController.instance;
        detectionArea = GetComponent<SphereCollider>();

        if (enemySpawners == null || enemySpawners.Length == 0) {
            Debug.LogWarning("Add some spawners to the activator");
        }
    }


    void Update() {
        // If the player is in the range of the activator then the spawners are activated
        if (Vector3.Distance(player.transform.position, transform.position) < detectionArea.radius){
            foreach (var spawner in enemySpawners) {
                // Check if another activator has already activated the spawner
                if (spawner.enabled)
                    StartCoroutine(spawner.EnemyDrop());
            }
            enabled = false;
        }
    }
}
