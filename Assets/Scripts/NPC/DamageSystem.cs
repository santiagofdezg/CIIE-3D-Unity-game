
using UnityEngine;

public class DamageSystem : MonoBehaviour
{
    public float cooldownDmg = 1f; //en segundos
    float untilNextDmg = 0;
    public int maxDmg = 20;

//esta funcion chamase continuamente mentras dous colliders colisionen
    void OnTriggerStay(Collider hit){
         
        if (hit.gameObject.tag == "Player"){
            if(untilNextDmg<=0){
                hit.gameObject.GetComponent<PlayerHealthSystem>().TakeDamage(maxDmg);
                untilNextDmg = cooldownDmg;
            } else {
                untilNextDmg -= Time.deltaTime;
            }
        }

    }

    
}