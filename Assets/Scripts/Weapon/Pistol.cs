using UnityEngine;

public class Pistol : Weapon 
{

    public override void Shoot(){
        RaycastHit hit;
        flash.Play();
        audioManager.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 
        if (Physics.Raycast(cam.transform.position , cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            //PROGRAMAR OS DMGS
            //GameObject target = hit.transform.GetComponent<GameObject>();
        }
    }


}