﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private Subject PauseMenuObserver; 


    public static GameHandler instance;

    void Start() {
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
}
