
using UnityEngine;

public class WeaponManager : MonoBehaviour {
    
    [SerializeField]
    private GameObject[] weapons;
    public int selectedWeapon = 0;

    

    void Start() {
        InitializeWeapons();

        // SelectWeapon();
    }
    
    void InitializeWeapons() {
        for(int i=0; i < weapons.Length; i++) {
            weapons[i].SetActive(false);
        }
        weapons[selectedWeapon].SetActive(true);
    }


    // void Update() {
    //     int previousSelectedWeapon = selectedWeapon;

    //     if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Weapon Next")) {
    //         if (selectedWeapon >= transform.childCount -1)
    //             // After reaching the last weapon, return to the first one
    //             selectedWeapon = 0;
    //         else 
    //             selectedWeapon++;
    //     }
    //     if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Weapon Previous")) {
    //         if(selectedWeapon <= 0)
    //             // After reaching the first weapon, return to the last one
    //             selectedWeapon = transform.childCount - 1;
    //         else
    //             selectedWeapon--;
    //     }

    //     if (previousSelectedWeapon != selectedWeapon) {
    //         SelectWeapon();
    //     }

    // }

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
    }

    void SwitchWeapons(int newIndex) {
        weapons[selectedWeapon].SetActive(false);
        weapons[newIndex].SetActive(true);
        selectedWeapon = newIndex;
    }

    // void SelectWeapon() {

    //     int i = 0;
    //     foreach (Transform weapon in transform)
    //     {
    //         if (i == selectedWeapon)
    //             weapon.gameObject.SetActive(true);
    //         else
    //             weapon.gameObject.SetActive(false);
    //         i++;
    //     }
    // }
}
