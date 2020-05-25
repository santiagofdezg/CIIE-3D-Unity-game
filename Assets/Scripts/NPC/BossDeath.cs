using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Enemy;

public class BossDeath : MonoBehaviour {
    
    
    // void OnDestroy() {
    //     GameHandler.instance.onBossDied -= Disable;

    // }
    // void Start() {
    //     GameHandler.instance.onBossDied += Disable;
    // }

    // Update is called once per frame
    void Update() {
        if(!GetComponent<AIEnemy>().enabled){
            GetComponent<Narrate.NarrationCountElement>().Increment(1f);
            enabled = false;
        }
    }
}
