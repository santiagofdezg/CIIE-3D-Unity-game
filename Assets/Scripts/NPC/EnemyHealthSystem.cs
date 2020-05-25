using UnityEngine;
using UnityEngine.AI;
using Characters.Enemy;
using System;

public class EnemyHealthSystem : HealthSystem {

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

        if (String.IsNullOrEmpty(death_sound))
            death_sound = "zombie_death";

        if (String.IsNullOrEmpty(hurt_sound))
            hurt_sound = "zombie_hurt";
   
    }

    public override void TakeDamage(int damage){

        // Debug.Log(hurt_sound);
        AudioManager.instance.Play(hurt_sound, gameObject, true, gameObject.GetInstanceID());

        if (currentHealth > 0){
            currentHealth -= damage;
            aiEnemy.OnAware();
            // Debug.Log("New enemy health: " + currentHealth);
        } else {
            Kill();
        }
    }

    public void Kill() {
        AIEnemy aiEnemy = GetComponent<AIEnemy>();
        
        AudioManager.instance.Stop(aiEnemy.idleSound, aiEnemy.zombieID);
        AudioManager.instance.Play(death_sound, aiEnemy.gameObject, true, aiEnemy.zombieID);

        animator.SetBool("isDead", true);
        aiEnemy.enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<DamageSystem>().enabled = false;
        enabled = false;

        // Check if it is the Boss
        if (GetComponent<BossDeath>() != null)
            GameHandler.instance.bossDeath();
    }

}