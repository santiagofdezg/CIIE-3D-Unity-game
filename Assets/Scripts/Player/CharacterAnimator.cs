using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterAnimator : MonoBehaviour {

    public Animator animator;
    bool isGrounded;
    bool isJumping;

    float verticalSpeed = 0;
    // float locomationSmooth = 0.1f;


    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();    
    }

    // Update is called once per frame
    void Update() {    //collemos inputs
        float ver = Input.GetAxis("Vertical");
        float hor = Input.GetAxis("Horizontal");

        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        //Si lle damos ao shift estamos sprintando, as animacions ejecutanse ao 100
        //si non ao 80 para dar sensacion de non correr
        if (Input.GetKey(KeyCode.LeftShift)){
            animator.SetFloat("vertical", ver, 0.1f, Time.deltaTime);
            animator.SetFloat("horizontal", hor, 0.1f, Time.deltaTime);
        } else {
            animator.SetFloat("vertical", ver * 0.8f, 0.1f, Time.deltaTime);
            animator.SetFloat("horizontal", hor * 0.8f, 0.1f, Time.deltaTime);
        } 
        
        //variables de animator
        animator.SetFloat("lookHorizontal", mouseX, 0.1f, Time.deltaTime);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", verticalSpeed); 
    }

    //funcions para chamar desde outros scripts (setters)
    public void setGrounded(bool state) {
        isGrounded = state;
    }
    
    public void setTriggerJump() {
        animator.SetTrigger("isJumping");
    }

    public void setVerticalSpeed(float speed) {
        verticalSpeed = speed;
    }

}
