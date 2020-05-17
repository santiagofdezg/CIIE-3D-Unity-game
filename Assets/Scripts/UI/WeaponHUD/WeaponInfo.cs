
using UnityEngine;

public class WeaponInfo : MonoBehaviour {

    public WeaponManager weaponManager;
    private Weapon weapon;
    [HideInInspector]
    public int currentAmmo;
    [HideInInspector]
    public int maxAmmo;
    [HideInInspector]
    public Sprite weaponImage;


    void Start() {
        weapon = weaponManager.GetCurrentWeapon().GetComponent<Weapon>();
        currentAmmo = weapon.GetCurrentAmmo();
        maxAmmo = weapon.maxAmmo;
        weaponImage = weapon.weaponHUDImage;
    }

    public void updateInfo(Weapon newWeapon){
        weapon = newWeapon;
        currentAmmo = weapon.GetCurrentAmmo();
        maxAmmo = newWeapon.maxAmmo;
        weaponImage = weapon.weaponHUDImage;
    }


}
