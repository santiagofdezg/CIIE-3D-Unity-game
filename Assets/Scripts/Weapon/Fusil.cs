using UnityEngine;

public class Fusil : Weapon
{

    private float FireRate = 10f;
    private float lastfired;


    public override void Start(){
        base.Start();
        damage = 25;
        shotSoundIntensity = 25f;
        shotSoundName = "Shot_fusil";
        ammoReloadingSoundName = "Ammo_reloading_fusil";
        //Añadir observer ao subject
        //TODO: Esto e moi lento, ainda que solo se fai 1 vez, recomendable añadilos dendo o inspector?
        //Crear un manager que notifique?
        foreach (var obs in FindObjectsOfType<PauseMenu>()){
            obs.RegisterObserver(this);
        }       
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
