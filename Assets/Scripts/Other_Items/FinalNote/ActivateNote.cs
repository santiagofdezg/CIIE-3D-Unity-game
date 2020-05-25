
using UnityEngine;

public class ActivateNote : MonoBehaviour {
    
    void OnDestroy() {
        GameHandler.instance.onBossDied -= ActivateObject;

    }
    void Start() {
        gameObject.SetActive(false);
        GameHandler.instance.onBossDied += ActivateObject;
    }

    void ActivateObject() {
        gameObject.SetActive(true);
    } 
}
