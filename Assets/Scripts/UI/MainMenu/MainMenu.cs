﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    
    int musicID = 0;

    public void PlayGame() {
       //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); 
       AudioManager.instance.StopAll();
       GameLoader.instance.LoadGame();
    }

    public void QuitGame() {
        // Debug.Log("Quit");
        Application.Quit();
    }

    void Start(){
        AudioManager.instance.Play("Menu", false, musicID);
    }

    public void PlayClickAudio(){
        AudioManager.instance.Play("Click", true, musicID);


    }

}
