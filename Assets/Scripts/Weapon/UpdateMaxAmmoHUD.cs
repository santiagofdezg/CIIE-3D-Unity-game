
using UnityEngine;

public class UpdateMaxAmmoHUD : MonoBehaviour {

    private TMPro.TextMeshProUGUI maxAmmoText;
    private Weapon weapon;
    
    // Start is called before the first frame update
    void Start() {
        weapon = GetComponentInParent<Weapon>();
        maxAmmoText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update() {
        maxAmmoText.text = weapon.maxAmmo.ToString("0");
    }
}
