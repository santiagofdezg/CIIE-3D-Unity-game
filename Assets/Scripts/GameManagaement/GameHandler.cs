
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour {
    
    private Subject PauseMenuObserver; 
    public static GameHandler instance;

    //Usamos patrón observador pero utilizando eventos, mellor que herencia
    public event Action onPlayerDied;
    public event Action onBossDied;

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

    public void playerDeath(){
        if(onPlayerDied != null){
            onPlayerDied();
        }        
    }

    public void bossDeath(){
        if(onBossDied != null){
            onBossDied();
        }
    }



}
