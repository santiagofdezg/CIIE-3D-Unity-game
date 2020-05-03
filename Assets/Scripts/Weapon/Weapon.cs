using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Salidabala;
    public float range = 100f;
    public Camera cam;
    public ParticleSystem flash;

    // Manage sounds
    public AudioManager audioManager;

    [HideInInspector]
    public PlayerNoise playerNoiseManager;
    // ShotSoundIntensity is a distance unit. If the player is closer that this 
    // distance, the zombie will hear the sound. It can be different for each
    // type of weapon.
    public float shotSoundIntensity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        playerNoiseManager = GetComponent<PlayerNoise>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }


    public virtual void Shoot(){}

    
}
