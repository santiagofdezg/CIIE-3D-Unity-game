
using UnityEngine;

public class UpdateCurrentAmmoHUD : MonoBehaviour {

    private TMPro.TextMeshProUGUI currentAmmoText;
    private WeaponInfo weaponInfo;
    
    
    void Start() {
        weaponInfo = GetComponentInParent<WeaponInfo>();
        currentAmmoText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update() {
        currentAmmoText.text = weaponInfo.currentAmmo.ToString("0");
    }
}
