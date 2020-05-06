using UnityEngine;
using UnityEngine.AI;
using Characters.Enemy;

public class EnemyHealthSystem : HealthSystem
{

    void Start(){
        currentHealth = maxHealth;
    }

    
    public override void TakeDamage(int damage){
        if (currentHealth > 0){
            currentHealth -= damage;
            Debug.Log("New enemy health: " + currentHealth);
        } else {
            Die();
        }
    }


    void Die() {
        AIEnemy aiEnemy = GetComponent<AIEnemy>();
        aiEnemy.Kill();
        aiEnemy.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        enabled = false;
    }

}