using UnityEngine;
using System.Collections;
using EZCameraShake;

public class PlayerHealthSystem : HealthSystem
{

  
    bool isRegenHealth;
    public int healPerTime = 2;
    public float regenerationDelay = .5f;

    public float mgnShk=4f, rghShk=4f, fInShk=.1f, fOutShk=1f;




    void Start(){
        currentHealth = maxHealth;  
        HUD.instance.healthBar.SetMaxHealth(maxHealth);

 
        //Añadir observer ao subject
         GameHandler.instance.RegisterObserverPause(this);   
    }


    void Update() {
        if(currentHealth != maxHealth && !isRegenHealth) 
            StartCoroutine(RegenHealthOverTime());
    }

    
    public override void TakeDamage(int damage){
        if (currentHealth > 0){
            CameraShaker.Instance.ShakeOnce(mgnShk,rghShk,fInShk,fOutShk);
            currentHealth -= damage;

            if (currentHealth<=0) //nunca baixamos a vida de 0
                currentHealth = 0;

            HUD.instance.healthBar.SetHealth(currentHealth);
            HUD.instance.hitOverlay.flashScreen();
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
            HUD.instance.healthBar.SetHealth(currentHealth);
            yield return new WaitForSeconds (regenerationDelay);
        }
        isRegenHealth = false;
    }
  
}