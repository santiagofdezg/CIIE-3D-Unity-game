using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterAnimator : MonoBehaviour {

    public Animator animator;
    bool isGrounded;
    bool isJumping;

    float verticalSpeed = 0;
    // float locomationSmooth = 0.1f;

    private TerrainDetector terrainDetector;


    public void playerDeath(){
        animator.SetTrigger("isDead");
    }

    // Start is called before the first frame update
    void Start() {
        animator = GetComponentInChildren<Animator>();    
        terrainDetector = new TerrainDetector();

    }

    public void walkAnimation(float hor, float ver, bool isRunning){
        if(isRunning){
            animator.SetFloat("vertical", ver, 0.1f, Time.deltaTime);
            animator.SetFloat("horizontal", hor, 0.1f, Time.deltaTime);
        } else {
            animator.SetFloat("vertical", ver * 0.8f, 0.1f, Time.deltaTime);
            animator.SetFloat("horizontal", hor * 0.8f, 0.1f, Time.deltaTime);  
        }
    }

    public void updateAnimator(float mouseX){
        //variables de animator
        animator.SetFloat("lookHorizontal", mouseX, 0.1f, Time.deltaTime);
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("verticalSpeed", verticalSpeed); 
    }



    // Update is called once per frame
    void Update() {    //collemos inputs
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");


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

    private void StepJ(){
            Debug.Log("ole");
            soundOnTerrain();
    }

    private void StepV(){
        float hor = animator.GetFloat("horizontal");
        float ver = animator.GetFloat("vertical");

        if(ver>= 0.5f || ver<=-0.5f )
            soundOnTerrain();
    }

    
    private void StepH(){
        float hor = animator.GetFloat("horizontal");
        float ver = animator.GetFloat("vertical");

        if( hor >= 0.5f || hor<=-0.5f )
            soundOnTerrain();
    }

    private void StepTurn(){
        float hor = animator.GetFloat("lookHorizontal");

        
        if( hor >= 0.1f ||hor<=-0.1f ){
            soundOnTerrain();
        }

            
    }

    private void soundOnTerrain(){
        int indexTerrain = terrainDetector.GetActiveTerrainTextureIdx(transform.position);

        if (indexTerrain==0){
            AudioManager.instance.Play("step_grass", true, 0);

        } else if (indexTerrain==1 || indexTerrain == 2){
            AudioManager.instance.Play("step_dirt", true, 0);
        } else {
            AudioManager.instance.Play("step_cemento", true, 0);
        }



    }

    private void Death(){
        AudioManager.instance.StopAll();
        SceneManager.LoadScene(1);
    }


}
