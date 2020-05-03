using UnityEngine;

public class Pistol : Weapon 
{


      void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetButtonDown("Fire1")){
       
       
                Shoot();
            }
        

    }

    void Shoot(){
        RaycastHit hit;
        flash.Play();
        if (Physics.Raycast(cam.transform.position , cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            
            //PROGRAMAR OS DMGS
            //GameObject target = hit.transform.GetComponent<GameObject>();
        }
    }






















}