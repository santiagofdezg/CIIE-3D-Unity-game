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
            
            //PROGRAMAR OS DMGS
            //GameObject target = hit.transform.GetComponent<GameObject>();
        }
    }


}