using UnityEngine;

public class Pistol : Weapon 
{

    public override void Shoot(){
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 
        if (Physics.Raycast(cam.transform.position , cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Debug.DrawLine(cam.transform.position, hit.point, Color.red);
            
            //PROGRAMAR OS DMGS
            
            if (hit.transform.tag == "Enemy")
                hit.transform.GetComponent<HealthSystem>().TakeDamage(20);
        }
    }


}