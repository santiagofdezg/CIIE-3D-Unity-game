using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingConeWeapon : MonoBehaviour {

    public Weapon objectToPick;


    void Update() {
        if (objectToPick == null || !objectToPick.gameObject.activeInHierarchy){
            gameObject.SetActive(false);
        }
    }
}
