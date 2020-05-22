using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupPanel : MonoBehaviour


{

    
    public void OpenPanel(string text){

        gameObject.SetActive(true);

    }

    public void CloseMessagePanel(){
        gameObject.SetActive(false);
    }



}
