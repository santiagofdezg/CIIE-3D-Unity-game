using UnityEngine;
using UnityEngine.AI;
using Characters.Enemy;

public class EnemyHealthSystem : HealthSystem
{

    private Animator animator;
    private AIEnemy aiEnemy;

    void Start(){
        currentHealth = maxHealth;
        animator = GetComponentInChildren<Animator>();
        aiEnemy = GetComponent<AIEnemy>();

        //Añadir observer ao subject
        //TODO: Esto e moi lento, ainda que solo se fai 1 vez, recomendable añadilos dendo o inspector?
        //Crear un manager que notifique?
        foreach (var obs in FindObjectsOfType<PauseMenu>()){
            obs.RegisterObserver(this);
        }       
    }

    
    public override void TakeDamage(int damage){
        if (currentHealth > 0){
            currentHealth -= damage;
            aiEnemy.OnAware();
            Debug.Log("New enemy health: " + currentHealth);
        } else {
            Die();
        }
    }


    void Die() {
        AIEnemy aiEnemy = GetComponent<AIEnemy>();
        animator.SetBool("isDead", true);
        aiEnemy.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<DamageSystem>().enabled = false;
        enabled = false;
    }

}