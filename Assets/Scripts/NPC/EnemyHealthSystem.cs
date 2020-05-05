using UnityEngine;

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
            //Aqui matamos ao enemigo basico
            Debug.Log("Enemigo morto");
        }
    }

}