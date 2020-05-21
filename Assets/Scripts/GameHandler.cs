
using UnityEngine;

public class GameHandler : MonoBehaviour {
    
    private Subject PauseMenuObserver; 
    public static GameHandler instance;

    void Awake() {
        //Singleton
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
        }

        //Obter subject pause
        PauseMenuObserver = FindObjectOfType<PauseMenu>();
    }

    public void RegisterObserverPause(Observer obs) {
        PauseMenuObserver.RegisterObserver(obs);
    }
    public void UnregisterObserverPause(Observer obs) {
        PauseMenuObserver.UnregisterObserver(obs);
    }
}
