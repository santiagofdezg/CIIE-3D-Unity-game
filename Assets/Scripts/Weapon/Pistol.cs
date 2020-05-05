using UnityEngine;

public class Pistol : Weapon 
{
    public override void Start(){
        base.Start();
        damage = 20f;
        shotSoundIntensity = 20f;
    }

    public override void Shoot(){
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 
        if (Physics.Raycast(FirstPersonCam.transform.position , FirstPersonCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy") {
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                target.TakeDamage(damage);
            }
        }
    }


}