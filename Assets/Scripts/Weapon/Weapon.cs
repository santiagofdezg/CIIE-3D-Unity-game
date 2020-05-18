using UnityEngine;
using UnityEngine.UI;
using Characters.ThirdPersonCharacter;
using System.Collections;


public class Weapon : Observer
{
    private ThirdPersonCharacterController tpcc;
    public Sprite weaponHUDImage;
    
    // Weapon damage
    public float range = 100f;
    public int damage = 10;

    // Cameras management
    [HideInInspector]
    public Camera currentCamera;
    [HideInInspector]
    public bool thirdPersonCamFlag;

    // Shooting effect
    public ParticleSystem flash;
    public Transform Salidabala;

    // Bullet impact effects
    public GameObject impactEffectEnemy;
    public GameObject impactEffectSurface;

    // Manage sounds
    [HideInInspector]
    public PlayerNoise playerNoiseManager;
    // ShotSoundIntensity is a distance unit. If the player is closer that this 
    // distance, the zombie will hear the sound. It can be different for each
    // type of weapon.
    public float shotSoundIntensity = 20f;
    
    public string shotSoundName = "Shot_pistol";

    // Ammo & reloading
    public int maxAmmo = 10;
    private int currentAmmo;
    public float reloadTime = 2.2f;
    [HideInInspector]
    public bool isReloading = false;
    
    public string ammoReloadingSoundName = "Ammo_reloading";

    public int reloadID = 0;
    public int shootID = 0;


    public override void OnNotify(NotificationType notificationType){
        if (notificationType == NotificationType.Paused){
            AudioManager.instance.Pause(ammoReloadingSoundName, reloadID);
            this.enabled = false;
        } else if (notificationType == NotificationType.UnPaused){
            AudioManager.instance.UnPause(ammoReloadingSoundName, reloadID);
            this.enabled = true;
        }
    }


    // Start is called before the first frame update
    public virtual void Start() {
        playerNoiseManager = GetComponentInParent<PlayerNoise>();
        tpcc = GetComponentInParent<ThirdPersonCharacterController>();
        currentCamera = tpcc.currentCamera;
        thirdPersonCamFlag = tpcc.IsThirdPersonCameraActive();
        currentAmmo = maxAmmo;

        //añadimos este observer ao subject pause
        GameHandler.instance.RegisterObserverPause(this); 
    }


    public void updateCamera() {
        // Depending on the camera, the crosshair may change
        currentCamera = tpcc.currentCamera;
        thirdPersonCamFlag = tpcc.IsThirdPersonCameraActive();
    }


    public virtual void checkShootingButton() {
        if (Input.GetButtonDown("Fire1"))
            Shoot();
    }


    // Update is called once per frame
    public virtual void Update() {
        updateCamera();

        if (!isReloading) {       
            if (currentAmmo <=0 ) {
                StartCoroutine(Reload());  
            } else {
                checkShootingButton();
            }
        }
    }


    IEnumerator Reload() {
        AudioManager.instance.Play(ammoReloadingSoundName, gameObject, false, reloadID);
        isReloading = true;
        yield return new WaitForSeconds(reloadTime);
        // Debug.Log("hEY");
        currentAmmo = maxAmmo;
        isReloading = false;
    }


    public virtual void Shoot(){
        currentAmmo--;
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play(shotSoundName, gameObject.transform.position, true, currentAmmo);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 

        // Debug.Log(shotSoundName);

        // Ignore the Player layer, so we get the mask and then it is inverted
        int playerLayerMask = ~LayerMask.GetMask("Player");
        if (Physics.Raycast(currentCamera.transform.position , currentCamera.transform.forward, out hit, range, playerLayerMask)) {
            // Debug.Log(hit.transform.name);
            GameObject impact;
            if (hit.transform.tag == "Enemy") {
                EnemyHealthSystem target = hit.transform.GetComponent<EnemyHealthSystem>();
                target.TakeDamage(damage);
                impact = Instantiate(impactEffectEnemy, hit.point, Quaternion.LookRotation(hit.normal));
            } else {
                impact = Instantiate(impactEffectSurface, hit.point, Quaternion.LookRotation(hit.normal));
            }
            Destroy(impact, 1.5f);
        }
    }

    public int GetCurrentAmmo() {
        return currentAmmo;
    }

    
}
