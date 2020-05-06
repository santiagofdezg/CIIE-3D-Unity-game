using UnityEngine;

public class Pistol : Weapon 
{
    public override void Start(){
        base.Start();
        damage = 20;
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
            GameObject impact;
            if (hit.transform.tag == "Enemy") {
                EnemyHealthSystem target = hit.transform.GetComponent<EnemyHealthSystem>();
                target.TakeDamage(damage);
                impact = Instantiate(impactEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            } else {
                impact = Instantiate(impactEffectSurface, hit.point, Quaternion.LookRotation(hit.normal));
            }
            Destroy(impact, 2.5f);
        }
    }


}