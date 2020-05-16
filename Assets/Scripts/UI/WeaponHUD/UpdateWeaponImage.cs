
using UnityEngine;
using UnityEngine.UI;

public class UpdateWeaponImage : MonoBehaviour {

    private WeaponInfo weaponInfo;
    private Image weaponImage;

    void Start() {
        weaponInfo = GetComponentInParent<WeaponInfo>();
        weaponImage = GetComponent<Image>();
    }

    void Update() {
        weaponImage.sprite = weaponInfo.weaponImage;
    }
}
