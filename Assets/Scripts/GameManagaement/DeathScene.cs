using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{

    public string deathTheme = "DeathTheme";
    public string clickSound = "Click";
    
    int musicID = 0;

    public void RetryGame() {
       AudioManager.instance.Stop(deathTheme, musicID);
       GameLoader.instance.LoadLastScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void GoMenu() {
        //AudioManager.instance.Stop("Theme", musicID);
        AudioManager.instance.StopAll();
        GameLoader.instance.LoadMenu(SceneManager.GetActiveScene().buildIndex);
            
    }


    public void QuitGame() {
        // Debug.Log("Quit");
        Application.Quit();
    }

  

    void Start(){
        musicID = gameObject.GetInstanceID();
        Cursor.visible = true; //activa o cursor o cursor
        Cursor.lockState = CursorLockMode.None; //libera o cursor
        AudioManager.instance.Play(deathTheme, false, musicID);
    }

    public void PlayClickAudio(){
        AudioManager.instance.Play(clickSound, true, musicID);


    }


}
