using UnityEngine;
using System.Collections;

public class CurveCollider : MonoBehaviour
{

		private enum Direction
		{
				RIGHT,
				LEFT,
				STRAIGHT
		}

		// Use this for initialization
		void Start ()
		{
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				GameObject colliderObject = collider.attachedRigidbody.gameObject;
				//string colliderName = colliderObject.name.Replace ("(Clone)", "");
				//string myName = this.gameObject.name.Replace ("(Clone)", "");
				PlayerController playerController = collider.transform.GetComponent<PlayerController> ();
				if (playerController != null) {									
						Direction direction = getDirection (playerController.direction);
						if (direction.Equals (Direction.RIGHT)) {
								playerController.TurnRight ();
						} else if (direction.Equals (Direction.LEFT)) {
								playerController.TurnLeft ();
					
						}
				}
		}

		Direction getDirection (Vector2 playerDirection)
		{
				float rotation = this.transform.rotation.x;
				if (playerDirection.x == 1.0f) {
						if (rotation == 180.0f) {
								return Direction.RIGHT;
						}
						if (rotation == 270.0f) {
								return Direction.LEFT;
						}

				}

				if (playerDirection.x == -1.0f) {
						if (rotation == 0.0f) {
								return Direction.RIGHT;
						}
						if (rotation == 90.0f) {
								return Direction.LEFT;
						}
			
				}

				if (playerDirection.y == 1.0f) {
						if (rotation == 90.0f) {
								return Direction.RIGHT;
						}
						if (rotation == 180.0f) {
								return Direction.LEFT;
						}
			
				}

				if (playerDirection.y == -1.0f) {
						if (rotation == 0.0f) {
								return Direction.LEFT;
						}
						if (rotation == 270.0f) {
								return Direction.RIGHT;
						}
			
				}
				return Direction.STRAIGHT;
		}
}
