
using UnityEngine;
using Characters.ThirdPersonCharacter;
using UnityEngine.AI;

namespace Characters.Enemy {

	public class AIEnemy : MonoBehaviour {
		
		public ThirdPersonCharacterController tpcc;
		// Field of view for the zombie
		public float fieldOfView = 120f;
		public float viewDistance = 10f;
		
		private bool isAware = false;
		private NavMeshAgent agent;
		// Temporary attribute, just for debugging. It helps to know when the zombie is aware.
		private Renderer zombieRenderer;

		// For zombie wandering
		public float wanderRadius = 10f;
		// The point where the enemy is currently wandering to
		private Vector3 wanderPoint ; 

		public void Start() {
			agent = GetComponent<NavMeshAgent>();
			wanderPoint = RandomWanderPoint();
			// Just for debugging
			zombieRenderer = GetComponent<Renderer>();
		}

		public void Update() {
			if (isAware) {
				// This function makes the zombie chases the player
				agent.SetDestination(tpcc.transform.position);
				// Just for debugging
				zombieRenderer.material.color = Color.red;
			} else {
				SearchForPlayer();
				Wander();
				// Just for debugging
				zombieRenderer.material.color = Color.blue;
			}
		}
		
		public void SearchForPlayer() {
			// Check if the player is within the zombie viewing angle
			if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(tpcc.transform.position)) < (fieldOfView/2)){
				if (Vector3.Distance(tpcc.transform.position, transform.position) < viewDistance) {
					// Variable to save all the info about the raycast
					RaycastHit hit;
					if (Physics.Linecast(transform.position, tpcc.transform.position, out hit, -1)){
						Debug.Log(hit.transform.tag);
						if (hit.transform.CompareTag("Player")){
							// The detection is done through the model of the 
							// player because if we use the whole Player object
							// the linecast is calculated with the outer sphere
							// collider
							OnAware();
						}
					}
				}
			}
		}

		public void OnAware() {
			isAware = true;
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

		private void Wander() {

			if (Vector3.Distance(transform.position, wanderPoint) < 0.5f) {
				// If the enemy got to its wander point then we have to calculate 
				// another wander point so the enemy keeps going from one wander
				// point to another and so on.
				wanderPoint = RandomWanderPoint();
			} else {
				agent.SetDestination(wanderPoint);
			}
		}

	}

}
