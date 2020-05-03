using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Salidabala;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem flash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
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
