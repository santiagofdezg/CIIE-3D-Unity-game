using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthItem : MonoBehaviour
{

    public int healAmount = 75; //sobre 100
    int itemID = 0;

 
    void OnTriggerEnter(Collider hit){
        if (hit.gameObject.CompareTag("Player")){
               if (hit.gameObject.GetComponentInParent<PlayerHealthSystem>().Heal(healAmount)){
                    Destroy(gameObject);
                    AudioManager.instance.Play("Drink", true, itemID);
               }
        } 
    }
    

}
