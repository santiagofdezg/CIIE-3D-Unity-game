using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    public string endTheme = "EndTheme";
    void Start()
    {
        AudioManager.instance.Play(endTheme, false, gameObject.GetInstanceID());
        Cursor.visible = true; //activa o cursor o cursor
        Cursor.lockState = CursorLockMode.None; //libera o cursor
    }


    public void OnExitClick(){
        Application.Quit();
    }
}
