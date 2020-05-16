using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    public static HUD instance;
    // Start is called before the first frame update
    void Start()
    {
        //Singleton
        if (instance == null)
            instance = this;
        else{
            Destroy(gameObject);
        }



         
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
