﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.ThirdPersonCharacter {
    
    public class ThirdPersonCharacterController : Observer {

        //Singleton
        public static ThirdPersonCharacterController instance;

        public CharacterAnimator characterAnimator;
        public CharacterController controller;
        [Range(0,1)]
        public float airControl = 0.5f;
        
        WeaponManager weaponManager = null;

        private bool isWalking = true; // true=> walking; false=>running
        public float Speed = 10f;
        public float sprint = 1.5f;
        public float gravity = -20f;
        public float jumpHeight = 1f;
        public float backwardsMod = 0.3f;
        Vector3 velocity;

        // Estas variables son as respectivas a cámara.
        public float tCam = 0.2f;
        public float minCameraFOV = 75.0f;
        public float maxCameraFOV = 95.0f;
        public Camera thirdPersonCamera;
        public Camera firstPersonCamera;
        [HideInInspector]
        public Camera currentCamera;
        private bool thirdPersonCamFlag = true;

        //estas variables son pa ver si o personaje ahora mismo esta no suelo
        public Transform groundCheck;
        public float groundDistance = 0.4f; //distancia (radio) a que comproba o obxecto
        public LayerMask groundMask;
        bool isGrounded;

        private GameObject toPickup = null;
        private int playerID;

        public string pickupSound = "pickup";

        //Comprobar si o xogo esta pausado usando o observer
        public override void OnNotify(NotificationType notificationType) {
            if (notificationType == NotificationType.Paused){
                this.enabled = false;
            } else if (notificationType == NotificationType.UnPaused){
                this.enabled = true;
            }
        }

         void OnDestroy() {
             //Desuscribir observers
            GameHandler.instance.UnregisterObserverPause(this); 
            GameHandler.instance.onPlayerDied -= Die;

        }

        private void Die(){
            characterAnimator.playerDeath();
            this.enabled = false;
        }

        void Awake() {
            //Singleton
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            playerID = gameObject.GetInstanceID();
        }
        

        void Start() {

            // Activar a cámara inicial
            if (thirdPersonCamFlag) {
                currentCamera = thirdPersonCamera;
                thirdPersonCamFlag = true;
                thirdPersonCamera.enabled = true;
                firstPersonCamera.enabled = false;
            } else {
                currentCamera = firstPersonCamera;
                thirdPersonCamFlag = false;
                thirdPersonCamera.enabled = false;
                firstPersonCamera.enabled = true;
            }

            weaponManager = gameObject.GetComponentInChildren<WeaponManager>();
            characterAnimator = GetComponent<CharacterAnimator>(); //componente para animacions
            //thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            //firstPersonCamera = GetComponent<FirstPersonCamera>();

            //Añadir observer ao subject
            GameHandler.instance.RegisterObserverPause(this); 
            //Observer pero usando eventos
            GameHandler.instance.onPlayerDied += Die;

        }


        // Method to know how much noise is making the player
        public int GetPlayerStealthProfile() {
            if (isWalking) {
                return 0;
            } else {
                return 1;
            }
        }


        void pickupObject(){
            if (Input.GetButtonDown("Pickup") && toPickup != null){
                AudioManager.instance.Play(pickupSound, gameObject, true, playerID);
                weaponManager.AddWeapon(toPickup);
                HUD.instance.pickupPanel.CloseMessagePanel();
                toPickup = null;
            }
        }
        void PlayerMovement() {

            //Collemos o movimiento
            float hor = Input.GetAxis("Horizontal");
            //a velocidad de moverse de lado e mais lenta para que sexa coherente
            float ver = Input.GetAxis("Vertical") * 0.8f;

            Vector3 playerMovement = transform.right * hor + transform.forward * ver; //o movemento do xogador en coordenadas relativas
            
            if (ver < 0){ //hacia atras
                playerMovement *= backwardsMod;
            }

            if (isGrounded) { 
                //SOLO NOS MOVEMOS SI ESTAMOS EN EL SUELO
                if (Input.GetKey(KeyCode.LeftShift)){ //Shft pos sprint
                    isWalking = false;
                    controller.Move(playerMovement * Speed * Time.deltaTime * sprint); 
                     characterAnimator.walkAnimation(hor,ver,true);
                } else {
                    isWalking = true;
                    controller.Move(playerMovement * Speed * Time.deltaTime);
                    characterAnimator.walkAnimation(hor,ver,false);
                }

                if(Input.GetButtonDown("Jump")){
                    velocity.y = Mathf.Sqrt(jumpHeight*-2*gravity); //ecuacion do tiro vertical para o salto
                    characterAnimator.setTriggerJump();
                }
                
            } else {
                //MOVIMIENTO EN EL AIRE   
                controller.Move(playerMovement * Speed * Time.deltaTime * airControl);
            }

            if (isWalking) {
                currentCamera.fieldOfView = Mathf.Lerp(currentCamera.fieldOfView, minCameraFOV, tCam);
            } else {
                currentCamera.fieldOfView = Mathf.Lerp(currentCamera.fieldOfView, maxCameraFOV, tCam);
            }



            

        }  

        void checkCam(){
            if (Input.GetKeyDown(KeyCode.V)) {
                // cambios entre cámara
                if (!thirdPersonCamFlag) {
                    currentCamera = thirdPersonCamera;
                    thirdPersonCamFlag = true;
                    thirdPersonCamera.enabled = true;
                    firstPersonCamera.enabled = false;
                } else {
                    currentCamera = firstPersonCamera;
                    thirdPersonCamFlag = false;
                    thirdPersonCamera.enabled = false;
                    firstPersonCamera.enabled = true;
                }
            }
        }

        public bool IsThirdPersonCameraActive() {
            return thirdPersonCamFlag;
        }

        // Update is called once per frame
        void Update() {
            checkCam();
            //Para saber si estamos no suelo creamos unha mascara debaixo do personaje e miramos si
            //esa mascara colisiona ca layer ground, si o fai true si non false
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            
            //poeñmos a variable global ao que estemos nete frame
            characterAnimator.setGrounded(isGrounded);
            
            if (isGrounded && velocity.y < 0) { //mira si esta no suelo
                velocity.y = -2f; //si esta no suelo aplicamos unha forza pa mantelo pegado
            }

            PlayerMovement(); //chamamos ao movemento
            pickupObject();

            //aplicamos a gravedad
            velocity.y += gravity * Time.deltaTime; 

            characterAnimator.setVerticalSpeed(velocity.y); //para animacions

            // multiplicamos dos veces por deltatime por la ecuacion de la velocidad (1/2g * t^2)
            
            controller.Move(velocity * Time.deltaTime);
        }


            private void OnTriggerEnter(Collider other){
                 if (other.CompareTag("Weapon")){
                    HUD.instance.pickupPanel.OpenPanel("'F' para recoller");
                    toPickup = other.gameObject;

                 } 
            }

            private void OnTriggerExit(Collider other){

                if (other.CompareTag("Weapon")){
                    HUD.instance.pickupPanel.CloseMessagePanel();
                    toPickup = null;
                }
            }

       

        
    }

}

