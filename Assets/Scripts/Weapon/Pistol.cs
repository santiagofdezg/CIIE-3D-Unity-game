using UnityEngine;

public class Pistol : Weapon 
{
    public override void Start(){
        base.Start();
        damage = 20;
        shotSoundIntensity = 20f;
    }



}