
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject[] weapons = null;
    public int selectedWeapon = 0;

    
    void Start() {
        InitializeWeapons();
    }
    
    void InitializeWeapons() {
        for(int i=0; i < weapons.Length; i++) {
            weapons[i].SetActive(false);
        }
        weapons[selectedWeapon].SetActive(true);
    }

    void Update() {
        int newIndex = selectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Weapon Next")) {
            if (selectedWeapon >= weapons.Length -1)
                // After reaching the last weapon, return to the first one
                newIndex = 0;
            else 
                newIndex++;
            SwitchWeapons(newIndex);
            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Weapon Previous")) {
            if(selectedWeapon <= 0)
                // After reaching the first weapon, return to the last one
                newIndex = weapons.Length - 1;
            else
                newIndex--;
            SwitchWeapons(newIndex);

        }
        
        updateUI();
    }

    void updateUI(){
        HUD.instance.weaponInfo.updateInfo(weapons[selectedWeapon].GetComponent<Weapon>());
    }

    void SwitchWeapons(int newIndex) {
        weapons[selectedWeapon].GetComponent<Weapon>().isReloading = false;
        weapons[selectedWeapon].SetActive(false);
        weapons[newIndex].SetActive(true);
        selectedWeapon = newIndex;
    }

    public GameObject GetCurrentWeapon() {
        return weapons[selectedWeapon];
    }


}
