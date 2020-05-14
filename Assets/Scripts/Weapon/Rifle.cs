using UnityEngine;

public class Rifle : Weapon
{
    public override void Start(){
        base.Start();
        shotSoundName = "Shot_rifle";
        ammoReloadingSoundName = "Ammo_reloading_rifle";    
    }

}
