using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.ThirdPersonCharacter {
    
    public class ThirdPersonCharacterController : MonoBehaviour {

        public CharacterAnimator characterAnimator;
        public CharacterController controller;
        [Range(0,1)]
        public float airControl = 0.5f;
        
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
        private Camera actualCamera;
        private bool thirdPersonCamFlag;

        //estas variables son pa ver si o personaje ahora mismo esta no suelo
        public Transform groundCheck;
        public float groundDistance = 0.4f; //distancia (radio) a que comproba o obxecto
        public LayerMask groundMask;
        bool isGrounded;


        // Method to know how much noise is making the player
        public int GetPlayerStealthProfile() {
            if (isWalking) {
                return 0;
            } else {
                return 1;
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
                        
                    
                } else {
                  
                        isWalking = true;
                        controller.Move(playerMovement * Speed * Time.deltaTime);
                    
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
                actualCamera.fieldOfView = Mathf.Lerp(actualCamera.fieldOfView, minCameraFOV, tCam);
            } else {
                actualCamera.fieldOfView = Mathf.Lerp(actualCamera.fieldOfView, maxCameraFOV, tCam);
            }

        }  

        void checkCam(){
            if (Input.GetKeyDown(KeyCode.V)) {
              
           
         // cambios entre cámara

                if (actualCamera == firstPersonCamera) {
                    actualCamera = thirdPersonCamera;
                    thirdPersonCamera.enabled = true;
                    firstPersonCamera.enabled = false;
                } else {
                    actualCamera = firstPersonCamera;
                    thirdPersonCamera.enabled = false;
                    firstPersonCamera.enabled = true;
                }
            }
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

           
            
            //aplicamos a gravedad
            velocity.y += gravity * Time.deltaTime; 

            characterAnimator.setVerticalSpeed(velocity.y); //para animacions

            // multiplicamos dos veces por deltatime por la ecuacion de la velocidad (1/2g * t^2)
            
            controller.Move(velocity * Time.deltaTime);
            
        }

        void Start() {
            actualCamera = thirdPersonCamera;
            characterAnimator = GetComponent<CharacterAnimator>(); //componente para animacions
            //thirdPersonCamera = GetComponent<ThirdPersonCamera>();
            //firstPersonCamera = GetComponent<FirstPersonCamera>();
        }
    }

}

