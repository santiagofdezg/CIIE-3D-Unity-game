using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCameraController : MonoBehaviour
{

    float mouseSensitivity = 100f;
   
    public Transform Target, Player;
    float mouseX,mouseY;

    void Update() { //Late update se llama justo despues del Update
        CamControl();
    }


    void CamControl(){
        
        mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        mouseY = Mathf.Clamp(mouseY, -35, 60); // limita os angulos de movimientos da camara

        transform.LookAt(Target); //Hace que la camara siempre enfoque el objeto Target

        if (Input.GetKey(KeyCode.LeftAlt)){ //pulsando o shift solo rotamos a camara
        
              Target.rotation = Quaternion.Euler(mouseY, mouseX, 0); 

        } else {
           
            Target.rotation = Quaternion.Euler(mouseY, mouseX, 0); 
            Player.rotation = Quaternion.Euler(0, mouseX, 0);  //el jugador tambien rota con la camara
    

        }


       

    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false; //elimina o cursor
        Cursor.lockState = CursorLockMode.Locked; //centra o cursor

    }

}