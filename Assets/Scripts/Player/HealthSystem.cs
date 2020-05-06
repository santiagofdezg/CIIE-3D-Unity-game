using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
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


}
