using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Salidabala;
    public float range = 100f;
    public float damage = 10f;
    public Camera FirstPersonCam;
    public ParticleSystem flash;
    public GameObject impactEffectEnemy;
    public GameObject impactEffectSurface;

    // Manage sounds
    [HideInInspector]
    public PlayerNoise playerNoiseManager;
    // ShotSoundIntensity is a distance unit. If the player is closer that this 
    // distance, the zombie will hear the sound. It can be different for each
    // type of weapon.
    public float shotSoundIntensity = 20f;

    // Start is called before the first frame update
    public virtual void Start()
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


    public virtual void Shoot(){
    }

    
}
