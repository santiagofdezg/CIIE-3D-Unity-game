using UnityEngine;
using System.Collections;

public class PlayerHealthSystem : HealthSystem
{

    public HealthBar healthBar;
    bool isRegenHealth;
    public int healPerTime = 2;
    public float regenerationDelay = .5f;


    void Start(){
        currentHealth = maxHealth;  
        healthBar.SetMaxHealth(maxHealth);
    }


    void Update() {
        if(currentHealth != maxHealth && !isRegenHealth) 
            StartCoroutine(RegenHealthOverTime());
    }

    
    public override void TakeDamage(int damage){
        if (currentHealth > 0){
            currentHealth -= damage;
            if (currentHealth<=0) //nunca baixamos a vida de 0
                currentHealth = 0;

            healthBar.SetHealth(currentHealth);
            Debug.Log("New health: " + currentHealth);
        } else {
            //morriche
            Debug.Log("Player Died!");
        }
    }

    // Corrutina para regenerar vida
    private IEnumerator RegenHealthOverTime() {
        isRegenHealth = true;
        while (currentHealth < maxHealth) {
            Heal(healPerTime);
            healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds (regenerationDelay);
        }
        isRegenHealth = false;
    }
  
}