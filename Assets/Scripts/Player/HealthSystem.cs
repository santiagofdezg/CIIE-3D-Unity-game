using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : Observer
{
    public int maxHealth = 100;
    public int currentHealth;




    public virtual void TakeDamage(int damage){
        if (currentHealth > 0){
            currentHealth -= damage;
            Debug.Log("New health: " + currentHealth);
        } else {
            Destroy(gameObject);
        }
    }



    public void Heal(int healAmount){
        currentHealth += healAmount;
    }

     public override void OnNotify(NotificationType notificationType){
        if (notificationType == NotificationType.Paused){
            this.enabled = false;
        } else if (notificationType == NotificationType.UnPaused){
            this.enabled = true;
        }


     }


}
