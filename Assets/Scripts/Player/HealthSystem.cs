﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem : Observer {
    
    public int maxHealth = 100;
    public int currentHealth;

    public string hurt_sound = "";
    public string death_sound = "";


    public virtual void TakeDamage(int damage) {
        if (currentHealth > 0){
            currentHealth -= damage;
            // Debug.Log("New health: " + currentHealth);
        } else {
            Destroy(gameObject);
        }
    }

    public bool Heal(int healAmount) {
        if (currentHealth < maxHealth){
            currentHealth = Math.Min(currentHealth + healAmount, maxHealth);

            return true;

        } else {
            return false;
        }
    }

    public override void OnNotify(NotificationType notificationType) {
        if (notificationType == NotificationType.Paused){
            this.enabled = false;
        } else if (notificationType == NotificationType.UnPaused){
            this.enabled = true;
        }
    }


}
