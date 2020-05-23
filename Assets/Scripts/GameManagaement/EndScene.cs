using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("EndTheme", false, 0);
    }


    public void OnExitClick(){
        Application.Quit();
    }
}
