
using UnityEngine;
using Characters.Enemy;
using Characters.ThirdPersonCharacter;

public class PlayerNoise : MonoBehaviour {
    
    public AudioClip shootSound;
    // Distance unit. If the player is closer that this distance, the zombie
    // will hear the sound
    public float ShootSoundIntensity = 20f;
    public float walkEnemyPerceptionRadius = 1.5f;
    public float sprintEnemyPerceptionRadius = 4f;
    // For optimization
    public LayerMask zombieLayer;

    private AudioSource audioSource;
    private ThirdPersonCharacterController tpcc;
    private SphereCollider sphereCollider;

    public void Start() {
        audioSource = GetComponent<AudioSource>();
        tpcc = GetComponent<ThirdPersonCharacterController>();
        sphereCollider = GetComponent<SphereCollider>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)){
            // When the user makes a click
            Fire();
        }
        if (tpcc.GetPlayerStealthProfile() == 0){
            // The player is walking
            sphereCollider.radius = walkEnemyPerceptionRadius;
        } else {
            // The player is running
            sphereCollider.radius = sprintEnemyPerceptionRadius;
        }
    }

    // PLACE HERE THE LOGIC OF THE SHOOTING
    public void Fire(){
        audioSource.PlayOneShot(shootSound);
        // Array of zombies that are within the overlapped sphere of the player
        // OverlapSphere() is very compute-intensive, don't run it in Update()
        Collider[] zombies = Physics.OverlapSphere(transform.position, ShootSoundIntensity, zombieLayer);
        for (int i = 0; i < zombies.Length; i++) {
            zombies[i].GetComponent<AIEnemy>().OnAware();
        }
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Enemy")) {
            other.GetComponent<AIEnemy>().OnAware();
        }
    }

}
