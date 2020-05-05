
using UnityEngine;
using UnityEngine.AI;
using Characters.Enemy;

public class EnemyHealth : MonoBehaviour {
    
    public float health = 100f;
    private bool isDead = false;
    private AIEnemy aiEnemy;
    private NavMeshAgent navMeshAgent;

    void Start() {
        aiEnemy = GetComponent<AIEnemy>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void TakeDamage(float amount) {
        health -= amount;
        if (health <= 0 && !isDead) {
            Die();
            isDead = true;
        }
    }

    void Die() {
        aiEnemy.Kill();
        aiEnemy.enabled = false;
        navMeshAgent.enabled = false;
        enabled = false;
    }
}
