using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCharacterController : MonoBehaviour
{


    public CharacterAnimator characterAnimator;
    public CharacterController controller;
    public float Speed = 10f;
    public float sprint = 1.5f;
    public float gravity = -20f;
    public float jumpHeight = 1f;
    Vector3 velocity;

    //estas variables son pa ver si o personaje ahora mismo esta no suelo
    public Transform groundCheck;
    public float groundDistance = 0.4f; //distancia (radio) a que comproba o obxecto
    public LayerMask groundMask;
    bool isGrounded;


    void PlayerMovement(){


        //Collemos o movimiento
        float hor = Input.GetAxis("Horizontal");
        
        //a velocidad de moverse de lado e mais lenta para que sexa coherente
        float ver = Input.GetAxis("Vertical") * 0.8f;

 

        Vector3 playerMovement = transform.right * hor + transform.forward * ver; //o movemento do xogador en coordenadas relativas
    
    if (isGrounded) { //SOLO NOS MOVEMOS SI ESTAMOS EN EL SUELO
        if (Input.GetKey(KeyCode.LeftShift)){ //Shft pos sprint
            controller.Move(playerMovement * Speed * Time.deltaTime * sprint);

        } else {
            controller.Move(playerMovement * Speed * Time.deltaTime);

        }
    }
       
        if(Input.GetButtonDown("Jump") && isGrounded){  //Is grounded true cando tamos no suelo
            velocity.y = Mathf.Sqrt(jumpHeight*-2*gravity); //ecuacion do tiro vertical para o salto
            characterAnimator.setTriggerJump();
        }


       


    }  

    // Update is called once per frame

    void Update()
    {
        //Para saber si estamos no suelo creamos unha mascara debaixo do personaje e miramos si
        //esa mascara colisiona ca layer ground, si o fai true si non false
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        //poeñmos a variable global ao que estemos nete frame
        characterAnimator.setGrounded(isGrounded);

        
        if (isGrounded && velocity.y < 0){ //mira si esta no suelo
            velocity.y = -2f; //si esta no suelo aplicamos unha forza pa mantelo pegado
        }

        PlayerMovement(); //chamamos ao movemento
        
        //aplicamos a gravedad
        velocity.y += gravity * Time.deltaTime; 

        characterAnimator.setVerticalSpeed(velocity.y); //para animacions

        // multiplicamos dos veces por deltatime por la ecuacion de la velocidad (1/2g * t^2)
        controller.Move(velocity * Time.deltaTime);
        
    }

    void Start(){
        characterAnimator = GetComponent<CharacterAnimator>(); //componente para animacions
    }
}
