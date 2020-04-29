
using UnityEngine;
using Characters.Enemy;

public class PlayerNoise : MonoBehaviour {
    
    public AudioClip shootSound;
    // Distance unit. If the player is closer that this distance, the zombie
    // will hear the sound
    public float ShootSoundIntensity = 20f;
    // For optimization
    public LayerMask zombieLayer;

    private AudioSource audioSource;


    public void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void Update() {
        if (Input.GetMouseButtonDown(0)){
            // When the user makes a click
            Debug.Log("Fireee");
            Fire();
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

    
}
