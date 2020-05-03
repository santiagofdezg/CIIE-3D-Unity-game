using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Salidabala;
    public float range;
    public Camera cam;
    public ParticleSystem flash;

    // Manage sounds
    public AudioManager audioManager;
    private PlayerNoise playerNoiseManager;
    // ShotSoundIntensity is a distance unit. If the player is closer that this 
    // distance, the zombie will hear the sound. It can be different for each
    // type of weapon.
    public float shotSoundIntensity = 20f;

    // Start is called before the first frame update
    void Start()
    {
        playerNoiseManager = GetComponentInParent<PlayerNoise>();
    }

    // Update is called once per frame
    void Update()
    {
     
    }
 

    void Shoot(){}
    
}
