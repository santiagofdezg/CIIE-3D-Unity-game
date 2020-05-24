using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerScript : MonoBehaviour
{

    public bool isEnd = true;

void OnTriggerEnter(Collider other)
{
    if(other.gameObject.CompareTag("Player"))
    {
       if (isEnd){
        GameLoader.instance.LoadEndMenu(SceneManager.GetActiveScene().buildIndex);
       } else {
        GameLoader.instance.LoadNextScene(SceneManager.GetActiveScene().buildIndex);
       }
    }
}

}
