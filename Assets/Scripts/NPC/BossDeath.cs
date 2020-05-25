using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Characters.Enemy;

public class BossDeath : MonoBehaviour {
    
    
    void OnDestroy() {
        GameHandler.instance.onBossDied -= EnableEndGame;

    }
    void Start() {
        GameHandler.instance.onBossDied += EnableEndGame;
    }

    void EnableEndGame(){
        // Activate the text dialogs
        StartCoroutine(ShowDialog());
    }

    IEnumerator ShowDialog(){
        yield return new WaitForSeconds(4f);
        GetComponent<Narrate.NarrationCountElement>().Increment(1f);
    }

}
