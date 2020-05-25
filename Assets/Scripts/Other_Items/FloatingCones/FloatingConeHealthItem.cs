using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingConeHealthItem : MonoBehaviour {

    public HealthItem objectToPick;

    void Update() {
        if (objectToPick == null){
            gameObject.SetActive(false);
        }
    }
}
