
using UnityEngine;
 using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {
    
    [SerializeField]
    private List<GameObject> weapons = new List<GameObject>();
    public int selectedWeapon = 0;

    private const int NUM_WEAPONS = 3;

    void OnDestroy() {
        //desuscribir observer
        GameHandler.instance.onPlayerDied -= Disable;

    }
    void Start() {
        InitializeWeapons();
        GameHandler.instance.onPlayerDied += Disable;
    }

    private void Disable(){
        gameObject.SetActive(false);
    }
    
    void InitializeWeapons() {

        if (weapons != null && weapons.Count != 0 ){
            for(int i=0; i < weapons.Count; i++) {
                GameObject weapon = Instantiate(weapons[i], gameObject.transform, false);
                weapon.GetComponent<Weapon>().enabled = true;
                weapons[i] = weapon;
                weapon.SetActive(false);
            }
            weapons[selectedWeapon].SetActive(true);
        } else {
            weapons = new List<GameObject>();
        }
    }

    void Update() {
        int newIndex = selectedWeapon;

        if ( weapons.Count != 0){
            if (Input.GetAxis("Mouse ScrollWheel") > 0f || Input.GetButtonDown("Weapon Next")) {
                if (selectedWeapon >= weapons.Count -1)
                    // After reaching the last weapon, return to the first one
                    newIndex = 0;
                else 
                    newIndex++;
                SwitchWeapons(newIndex);
                
            }
            if (Input.GetAxis("Mouse ScrollWheel") < 0f || Input.GetButtonDown("Weapon Previous")) {
                if(selectedWeapon <= 0)
                    // After reaching the first weapon, return to the last one
                    newIndex = weapons.Count - 1;
                else
                    newIndex--;
                SwitchWeapons(newIndex);

            }
        }
        updateUI();
    }

    void updateUI(){
        if (weapons.Count>0){
            
            if (!HUD.instance.weaponInfo.gameObject.activeSelf)
                HUD.instance.weaponInfo.gameObject.SetActive(true);

            HUD.instance.weaponInfo.updateInfo(weapons[selectedWeapon].GetComponent<Weapon>());
        } else {
            if (HUD.instance.weaponInfo.gameObject.activeSelf)
                HUD.instance.weaponInfo.gameObject.SetActive(false);
        }
    }

    void SwitchWeapons(int newIndex) {
        weapons[selectedWeapon].GetComponent<Weapon>().isReloading = false;
        weapons[selectedWeapon].SetActive(false);
        weapons[newIndex].SetActive(true);
        selectedWeapon = newIndex;
    }

    public GameObject GetCurrentWeapon() {
        if (weapons.Count != 0)
            return weapons[selectedWeapon];
        return null;
    }

    public void AddWeapon(GameObject weaponInit){
            weaponInit.SetActive(false);
            GameObject weapon = Instantiate(weaponInit, gameObject.transform.position, gameObject.transform.rotation);
            weapon.transform.parent = gameObject.transform;
            weapon.GetComponent<Weapon>().enabled = true;
            weapon.GetComponent<Rigidbody>().detectCollisions = false;
            // Debug.Log(weapons.Count -1);
            weapon.SetActive(true);

            if (weapons.Count > 0)
                weapon.SetActive(false);
            
                      
            weapons.Add(weapon);


    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        // Debug.Log("add");
    }


}
