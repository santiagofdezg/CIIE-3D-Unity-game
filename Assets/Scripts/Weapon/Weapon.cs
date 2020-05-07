using UnityEngine;
using Characters.ThirdPersonCharacter;

public class Weapon : Observer
{

    public Transform Salidabala;
    public float range = 100f;
    public int damage = 10;
    private ThirdPersonCharacterController tpcc;
    [HideInInspector]
    public Camera currentCamera;
    [HideInInspector]
    public bool thirdPersonCamFlag;
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



    public override void OnNotify(NotificationType notificationType){
        if (notificationType == NotificationType.Paused){
            this.enabled = false;
        } else if (notificationType == NotificationType.UnPaused){
            this.enabled = true;
        }
    }
    // Start is called before the first frame update
    public virtual void Start()
    {
        playerNoiseManager = GetComponentInParent<PlayerNoise>();
        tpcc = GetComponentInParent<ThirdPersonCharacterController>();
        currentCamera = tpcc.currentCamera;
        thirdPersonCamFlag = tpcc.IsThirdPersonCameraActive();
    }

    public void updateCamera() {
        // Depending on the camera, the crosshair may change
        currentCamera = tpcc.currentCamera;
        thirdPersonCamFlag = tpcc.IsThirdPersonCameraActive();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        updateCamera();

        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }


    public virtual void Shoot(){
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 

        // Ignore the Player layer, so we get the mask and then it is inverted
        int playerLayerMask = ~LayerMask.GetMask("Player");
        if (Physics.Raycast(currentCamera.transform.position , currentCamera.transform.forward, out hit, range, playerLayerMask)) {
            Debug.Log(hit.transform.name);
            GameObject impact;
            if (hit.transform.tag == "Enemy") {
                EnemyHealthSystem target = hit.transform.GetComponent<EnemyHealthSystem>();
                target.TakeDamage(damage);
                impact = Instantiate(impactEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            } else {
                impact = Instantiate(impactEffectSurface, hit.point, Quaternion.LookRotation(hit.normal));
            }
            Destroy(impact, 2.5f);
        }

        
    }

    
}
