using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathScene : MonoBehaviour
{

    
    int musicID = 0;

    public void RetryGame() {
       AudioManager.instance.Stop("Theme", musicID);
       SceneManager.LoadScene(PlayerPrefs.GetString("lastScene")); 
    }

    public void GoMenu() {
        //AudioManager.instance.Stop("Theme", musicID);
        AudioManager.instance.StopAll();
        //menu scene é a primeira
        SceneManager.LoadScene(0); 
    }


    public void QuitGame() {
        // Debug.Log("Quit");
        Application.Quit();
    }

  

    void Start(){
        Cursor.visible = true; //activa o cursor o cursor
        Cursor.lockState = CursorLockMode.None; //libera o cursor
        AudioManager.instance.Play("Theme", false, musicID);
    }

    public void PlayClickAudio(){
        AudioManager.instance.Play("Click", true, musicID);


    }


}
