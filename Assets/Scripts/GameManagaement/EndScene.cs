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
    }


    public void OnExitClick(){
        Application.Quit();
    }
}
