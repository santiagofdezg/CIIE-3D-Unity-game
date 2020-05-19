
using UnityEngine;

public class DamageSystem : MonoBehaviour {
    
    public float cooldownDmg = 1f; //en segundos
    float untilNextDmg = 0;
    public int maxDmg = 20;


    // Esta funcion chamase continuamente mentras dous colliders colisionen
    void OnTriggerStay(Collider hit){
        if (hit.gameObject.tag == "Damage_Collider"){
            if(untilNextDmg <= 0){
                hit.gameObject.GetComponentInParent<PlayerHealthSystem>().TakeDamage(maxDmg);
                untilNextDmg = cooldownDmg;
            } else {
                untilNextDmg -= Time.deltaTime;
            }
        }
    }

    
}