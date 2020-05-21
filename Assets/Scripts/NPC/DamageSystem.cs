
using UnityEngine;

public class DamageSystem : MonoBehaviour {
    
    public float cooldownDmg = 1f; //en segundos
    float nextDmg = 0;
    public int maxDmg = 20;


    // Esta funcion chamase continuamente mentras dous colliders colisionen
    void OnTriggerStay(Collider hit){
        if (hit.gameObject.tag == "Damage_Collider"){
            if(Time.time >= nextDmg){
                hit.gameObject.GetComponentInParent<PlayerHealthSystem>().TakeDamage(maxDmg);
                nextDmg = Time.time + cooldownDmg;
            } 
        }
    }

    
}