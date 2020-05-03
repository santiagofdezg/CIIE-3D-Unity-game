
using UnityEngine;
using Characters.Enemy;
using Characters.ThirdPersonCharacter;

public class PlayerNoise : MonoBehaviour {
    
    // public AudioClip shootSound;
    
    // public float ShootSoundIntensity = 20f;
    // public AudioManager audioManager;
    public float walkEnemyPerceptionRadius = 2f;
    public float sprintEnemyPerceptionRadius = 4f;
    // For optimization
    public LayerMask zombieLayer;

    // private AudioSource audioSource;
    private ThirdPersonCharacterController tpcc;
    private SphereCollider sphereCollider;
    // private Weapon weapon;

    public void Start() {
        tpcc = GetComponent<ThirdPersonCharacterController>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void Update() {
        if (tpcc.GetPlayerStealthProfile() == 0){
            // The player is walking
            sphereCollider.radius = walkEnemyPerceptionRadius;
        } else {
            // The player is running
            sphereCollider.radius = sprintEnemyPerceptionRadius;
        }
    }

    // This function is called from the Weapon class and checks if the enemy
    // heard the shot
    public void isEnemyHearingShoot(float ShotSoundIntensity){
        // Array of zombies that are within the overlapped sphere of the player
        // OverlapSphere() is very compute-intensive, don't run it in Update()
        Collider[] zombies = Physics.OverlapSphere(transform.position, ShotSoundIntensity, zombieLayer); // ShotSoundIntensity is a distance unit. If the player is closer that this distance, the zombie will hear the sound
        for (int i = 0; i < zombies.Length; i++) {
            zombies[i].GetComponent<AIEnemy>().OnAware();
        }
    }

    // Method that check if the enemy is hearing the player. If it's true then
    // change the enemy state to "aware"
    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<AIEnemy>().OnAware();
        }
    }

}
