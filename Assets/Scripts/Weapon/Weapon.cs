﻿using UnityEngine;

public class Weapon : MonoBehaviour
{

    public Transform Salidabala;
    public float range = 100f;
    public int damage = 10;
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
        playerNoiseManager = GetComponentInParent<PlayerNoise>();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire1")){
            Shoot();
        }
    }


    public virtual void Shoot(){
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 
        if (Physics.Raycast(FirstPersonCam.transform.position , FirstPersonCam.transform.forward, out hit, range))
        {
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
