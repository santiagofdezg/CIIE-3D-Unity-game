
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    
    public int selectedWeapon = 0;
    

    void Start() {
        SelectWeapon();
    }


    void Update() {
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Weapon Next")) {
            if (selectedWeapon >= transform.childCount -1)
                // After reaching the last weapon, return to the first one
                selectedWeapon = 0;
            else 
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Weapon Previous")) {
            if(selectedWeapon <= 0)
                // After reaching the first weapon, return to the last one
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (previousSelectedWeapon != selectedWeapon) {
            SelectWeapon();
        }

    }

    void SelectWeapon() {

        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
