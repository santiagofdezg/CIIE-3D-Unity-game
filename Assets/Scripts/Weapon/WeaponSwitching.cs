
using UnityEngine;

public class WeaponSwitching : MonoBehaviour {
    
    public int selectedWeapon = 0;
    

    void Start() {
        SelectWeapon();
    }


    void Update() {
     //   int previousSelectedWeapon = selectedWeapon;
       // if (Input.GetAxis("Mouse ScrollWheel") > 0f){
         //   Debug.Log("ola");
           // // After reaching the last weapon, return to the first one
            //selectedWeapon = selectedWeapon++ % transform.childCount;
        //} else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
          //  Debug.Log("adios");
            // After reaching the first weapon, return to the last one
            //selectedWeapon = selectedWeapon-- % transform.childCount;
        //} 
        int previousSelectedWeapon = selectedWeapon;

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (selectedWeapon >= transform.childCount -1)
                selectedWeapon = 0;
            else 
                selectedWeapon++;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if(selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (previousSelectedWeapon != selectedWeapon) 
        {
            SelectWeapon();
        }
    

        
        // FAI O MESMO PERO CON MÁIS CÓDIGO
        // if (Input.GetAxis("Mouse ScrollWheel") > 0f){
        //     // After reaching the last weapon, return to the first one
        //     if (selectedWeapon >= transform.childCount -1)
        //         selectedWeapon = 0;
        //     else
        //         selectedWeapon++;
        // } else if (Input.GetAxis("Mouse ScrollWheel") < 0f) {
        //     // After reaching the first weapon, return to the last one
        //     if (selectedWeapon <= 0)
        //         selectedWeapon = transform.childCount - 1;
        //     else
        //         selectedWeapon--;
        // }
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
