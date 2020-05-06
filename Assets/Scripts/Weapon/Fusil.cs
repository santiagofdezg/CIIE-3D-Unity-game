using UnityEngine;

public class Fusil : Weapon
{

    private float FireRate = 10f;
    private float lastfired;


    public override void Start(){
        base.Start();
        damage = 25;
        shotSoundIntensity = 25f;
    }


    // Update is called once per frame
    public override void Update()
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

    public override void Shoot(){
        RaycastHit hit;
        flash.Play();
        AudioManager.instance.Play("Shot", gameObject, true);
        playerNoiseManager.isEnemyHearingShoot(shotSoundIntensity); 
        if (Physics.Raycast(FirstPersonCam.transform.position , FirstPersonCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            if (hit.transform.tag == "Enemy") {
                EnemyHealthSystem target = hit.transform.GetComponent<EnemyHealthSystem>();
                target.TakeDamage(damage);
            }
        }
    }
}
