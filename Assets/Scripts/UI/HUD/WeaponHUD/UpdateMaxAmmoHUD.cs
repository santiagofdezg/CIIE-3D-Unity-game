
using UnityEngine;

public class UpdateMaxAmmoHUD : MonoBehaviour {

    private TMPro.TextMeshProUGUI maxAmmoText;
    private WeaponInfo weaponInfo;
    
    
    void Start() {
        weaponInfo = GetComponentInParent<WeaponInfo>();
        maxAmmoText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    void Update() {
        maxAmmoText.text = weaponInfo.maxAmmo.ToString("0");
    }
}
