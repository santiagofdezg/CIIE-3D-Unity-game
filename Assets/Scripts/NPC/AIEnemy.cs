
using UnityEngine;
using Characters.ThirdPersonCharacter;
using UnityEngine.AI;

namespace Characters.Enemy {

	public class AIEnemy : MonoBehaviour {
		
		private ThirdPersonCharacterController player; // The Player controller

		public float fieldOfView = 120f; // Field of view for the enemy
		public float viewDistance = 10f;

		//-- ENEMY MOVEMENT
		private NavMeshAgent agent;

		// Just for debugging with Enemy object. It helps to know when the zombie is aware.
		// private Renderer zombieRenderer;

		//-- TYPES OF MOVEMENT
		public float wanderRadius = 10f;
		private Vector3 wanderPoint; // The point where the enemy is currently wandering to
		public float wanderSpeed = 0.5f; // For walking animation
		public float wanderAngularSpeed = 50f;
		public float chaseSpeed = 4f; // For running animation
		public float chaseAngularSpeed = 140f;

		//-- DETECT/LOSE THE PLAYER
		private bool isAware = false; // Two states: aware or unaware
		private bool isDetecting = false; // If the enemy is currently perceiving the player
		public float loseThreshold = 5f; // Time (in seconds) until the enemy lose the player after he stop detecting him
		private float loseTimer = 0;

		//-- TYPES OF AI / WANDERING MODES
		public enum WanderType { Random, Waypoint };
		public WanderType wanderType = WanderType.Random;
		public Transform[] waypoints; // Array of waypoints, only used when waypoint wandering is selected
		private int waypointIndex = 0;		
		private bool warningThrown = false; // The warnings when there are less than 2 waypoints will be thrown once

		//-- ANIMATIONS
		private Animator animator;


		//-- METHODS

		public void Start() {
			player = ThirdPersonCharacterController.instance;

			agent = GetComponent<NavMeshAgent>();
			wanderPoint = RandomWanderPoint();
			animator = GetComponentInChildren<Animator>(); // The animator is in the enemy model which is a child of the enemy object
			
			animator.SetBool("isDead", false);
			// Just for debugging with Enemy object
			// zombieRenderer = GetComponent<Renderer>();
		}

		public void Update() {
			if (isAware) {
				// This function makes the enemy chases the player
				agent.SetDestination(player.transform.position);
				// The variable Aware of the animator can provoke changes in the animations
				animator.SetBool("Aware", true);
				agent.speed = chaseSpeed;
				agent.angularSpeed = chaseAngularSpeed;

				if (!isDetecting) {
					loseTimer += Time.deltaTime;
					if (loseTimer >= loseThreshold) {
						isAware = false;
						loseTimer = 0;
					}
				}

				// Just for debugging with Enemy object
				// zombieRenderer.material.color = Color.red;
			} else {
				Wander();
				animator.SetBool("Aware", false);
				agent.speed = wanderSpeed;
				agent.angularSpeed = wanderAngularSpeed;
				
				// Just for debugging with Enemy object
				// zombieRenderer.material.color = Color.blue;
			}
			// This function is executed out of the if-else because it always
			// has to be executed due to the enemy can lose the player when it
			// is chasing him
			SearchForPlayer();
		}
		
		public void SearchForPlayer() {
			// Check if the player is within the enemy viewing angle
			if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(player.transform.position)) < (fieldOfView/2)){
				if (Vector3.Distance(player.transform.position, transform.position) < viewDistance) {
					// Variable to save all the info about the raycast
					RaycastHit hit;
					if (Physics.Linecast(transform.position, player.transform.position, out hit, -1)){
						if (hit.transform.CompareTag("Player")){
							// The detection is done through the outer sphere
							// collider of the player
							OnAware();
						} else {
							isDetecting = false;
						}
					} else {
						isDetecting = false;
					}
				} else {
					isDetecting = false;
				}
			} else {
				isDetecting = false;
			}
		}

		// Enable the "Aware" state of the enemy
		public void OnAware() {
			isAware = true;
			// Restart the player detection parameters
			isDetecting = true;
			loseTimer = 0;
		}


		public Vector3 RandomWanderPoint() {
			// It calculates a random position within a sphere
			Vector3 randomPoint = (Random.insideUnitSphere * wanderRadius) + transform.position;
			NavMeshHit navHit;
			NavMesh.SamplePosition(randomPoint, out navHit, wanderRadius, -1);
			// We only need the x and z position whitin the sphere. For the y 
			// position we take the value from the enemy.
			return new Vector3(navHit.position.x, transform.position.y, navHit.position.z);
		}


		public void Wander() {
			if (wanderType == WanderType.Random) {
				// Random wandering
				if (Vector3.Distance(transform.position, wanderPoint) < 1f) {
					// If the enemy got to its wander point then we have to calculate 
					// another wander point so the enemy keeps going from one wander
					// point to another and so on.
					wanderPoint = RandomWanderPoint();
				} else {
					agent.SetDestination(wanderPoint);
				}
			} else {
				// Waypoint wandering
				if (waypoints.Length >= 2) {
					// There should be more than 1 waypoint
					if (Vector3.Distance(waypoints[waypointIndex].position, transform.position) < 1f) {
						if (waypointIndex == waypoints.Length-1) {
							// If the enemy reach the last waypoint, start again
							waypointIndex = 0;
						} else {
							waypointIndex++;
						}
					} else {
						agent.SetDestination(waypoints[waypointIndex].position);
					}
				} else {
					if (!warningThrown){
						// Throw the warning just once
						warningThrown = true;
						Debug.LogWarning("Wandering mode set to 'Waypoint', please assign more than 1 waypoint to the AI: "+gameObject.name+". You can also change the wandering mode to 'Random'.");
						
						// But if we want the enemy to stay still in the same place we can set only 1 waypoint
						if (waypoints.Length == 1) {
							Debug.LogWarning("Just 1 waypoint assigned to '"+gameObject.name+"'. It will stay still in the same place when reaching that waypoint.");
							agent.SetDestination(waypoints[0].position);
						} else {
							// Set enemy AI to random wandering mode
							wanderType = WanderType.Random;
							Debug.LogWarning("There is no waypoints assigned to '"+gameObject.name+"'. Setting wandering type to 'Random'.");
						}
					}
				}				
			}	
		}

	}

}
