using UnityEngine;

public class Fusil : Weapon
{

    private float FireRate = 10f;
    private float lastfired;


    public override void Start(){
        base.Start();
        shotSoundName = "Shot_fusil";
        ammoReloadingSoundName = "Ammo_reloading_fusil";    
    }

    public override void checkShootingButton() {
        if (Input.GetButton("Fire1")) {
            if (Time.time - lastfired > 1 / FireRate) {
                lastfired = Time.time;
                Shoot();
            }
        }
    }



}
