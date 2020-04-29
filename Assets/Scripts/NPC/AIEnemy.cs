
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
		// Temporary attribute, just for debugging
		private Renderer zombieRenderer;

		public void Start() {
			agent = GetComponent<NavMeshAgent>();
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
				// Just for debugging
				zombieRenderer.material.color = Color.blue;
			}
		}
		
		public void SearchForPlayer() {
			// Check if the player is within the zombie viewing angle
			if (Vector3.Angle(Vector3.forward, transform.InverseTransformPoint(tpcc.transform.position)) < (fieldOfView/2)){
				if (Vector3.Distance(tpcc.transform.position, transform.position) < viewDistance) {
					OnAware();
				}
			}
		}

		public void OnAware() {
			isAware = true;
		}

	}

}
