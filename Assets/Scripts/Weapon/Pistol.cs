using UnityEngine;

public class Pistol : Weapon 
{
    public override void Start(){
        base.Start();
        damage = 20;
        shotSoundIntensity = 20f;
        //Añadir observer ao subject
        //TODO: Esto e moi lento, ainda que solo se fai 1 vez, recomendable añadilos dendo o inspector?
        //Crear un manager que notifique?
        foreach (var obs in FindObjectsOfType<PauseMenu>()){
            obs.RegisterObserver(this);
        }       
    }

    

}