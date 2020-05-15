
using UnityEngine;

public class UpdateCurrentAmmoHUD : MonoBehaviour {

    private TMPro.TextMeshProUGUI currentAmmoText;
    private Weapon weapon;
    
    // Start is called before the first frame update
    void Start() {
        weapon = GetComponentInParent<Weapon>();
        currentAmmoText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        currentAmmoText.text = weapon.GetCurrentAmmo().ToString("0");
    }
}
