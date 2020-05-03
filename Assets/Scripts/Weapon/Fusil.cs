using UnityEngine;

public class Fusil : Weapon
{

    private float FireRate = 10f;
    private float lastfired;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetButtonDown("Fire1"))
        if (Input.GetButton("Fire1"))
        {
            if (Time.time - lastfired > 1 / FireRate)
            {
                lastfired = Time.time;
                Shoot();
            }
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
