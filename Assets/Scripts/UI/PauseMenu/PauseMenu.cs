using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : Subject
{

    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public GameObject optionsMenuUI;
    
    //Singleton
    public static PauseMenu instance;




    void Start() 
    {
        //Singleton
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
        }




         
        
    }

    public void Resume(){
        Cursor.visible = false; //elimina o cursor
        Cursor.lockState = CursorLockMode.Locked; //centra o cursor

        //desactiva o menu de pause
        pauseMenuUI.SetActive(false);
        optionsMenuUI.SetActive(false);

        //tempo a velocidade normal outra vez
        Time.timeScale = 1f;
        isPaused = false;

        //Notificamos observers
        Notify(NotificationType.UnPaused);


    }

    void Pause(){
        Cursor.visible = true; //activa o cursor o cursor
        Cursor.lockState = CursorLockMode.None; //libera o cursor


        //activa o menu de pause
        pauseMenuUI.SetActive(true);
 
        //cambia o tempo a 0, polo que pause o xogo
        Time.timeScale = 0f;
        isPaused = true;

        //Notificamos observers
        Notify(NotificationType.Paused);

    }

    public void QuitGame(){
        Debug.Log("Quit");
        Application.Quit();
    }

    public void LoadMenu(){
        Time.timeScale = 1f; //volve a poñer o tempo a normal no menu
        SceneManager.LoadScene("MainMenu"); 

    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            if (isPaused){
                Resume();
            } else {
                Pause();
            }
        }
    }
}
