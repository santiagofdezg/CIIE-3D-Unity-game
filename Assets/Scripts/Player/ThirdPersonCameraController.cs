﻿
using UnityEngine;

namespace Characters.ThirdPersonCharacter {

    public class ThirdPersonCameraController : MonoBehaviour {

        public float mouseSensitivity = 100f;
        public float mouseX;
        public float mouseY;
        public float positiveClampAngle = 60;
        public float negativeClampAngle = 35;

        bool lockRotation = false;
    
        public Transform Target, Player;


        void OnDestroy() {
            //desuscribir observer
            GameHandler.instance.onPlayerDied -= Die;

        }
        // Start is called before the first frame update
        void Start() {
            Cursor.visible = false; //elimina o cursor
            Cursor.lockState = CursorLockMode.Locked; //centra o cursor
            GameHandler.instance.onPlayerDied += Die;
        }
        private void Die(){
            lockRotation = true;
        }


        void Update() {
            mouseX += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            mouseY -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            mouseY = Mathf.Clamp(mouseY, -negativeClampAngle, positiveClampAngle); // limita os angulos de movimientos da camara

            // transform.LookAt(Target); //Hace que la camara siempre enfoque el objeto Target

             if (lockRotation){ //pulsando o shift solo rotamos a camara     
                 Target.rotation = Quaternion.Euler(mouseY, mouseX, 0); 
             } else {           
                Target.rotation = Quaternion.Euler(mouseY, mouseX, 0); 
                Player.rotation = Quaternion.Euler(0, mouseX, 0);  //el jugador tambien rota con la camara
             }
        }




    }

}

