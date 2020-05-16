using UnityEngine;

public class Rifle : Weapon
{
    public override void Start(){
        base.Start();
        shotSoundName = "Shot_rifle";
        reloadID = 1;
        shootID = 1;
    }

}
