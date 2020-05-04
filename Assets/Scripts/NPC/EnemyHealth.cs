
using UnityEngine;
using UnityEngine.AI;
using Characters.Enemy;

public class EnemyHealth : MonoBehaviour {
    
    public float health = 100f;
    // private bool isDead = false;
    private AIEnemy aiEnemy;
    private NavMeshAgent navMeshAgent;

    void Start() {
        aiEnemy = GetComponent<AIEnemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0) {
            Die();
        }
    }

    void Die() {
        // isDead = true;
        aiEnemy.Kill();
        aiEnemy.enabled = false;
        navMeshAgent.enabled = false;
    }
}
