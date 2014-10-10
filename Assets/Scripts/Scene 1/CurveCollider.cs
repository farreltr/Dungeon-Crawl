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
						int rotation = Mathf.RoundToInt (this.transform.eulerAngles.z);
						Direction direction = getTurningDirection (playerController, rotation);
						if (direction.Equals (Direction.RIGHT)) {
								playerController.Turn45Right ();
						} else if (direction.Equals (Direction.LEFT)) {
								//playerController.TurnLeft ();
								playerController.Turn45Left ();
					
						}
				}
		}

		Direction getTurningDirection (PlayerController playerController, int rotation)
		{
				Vector2 direction = playerController.direction;
				if (direction == PlayerController.RIGHT) {
						if (rotation == 180) {
								return Direction.RIGHT;
						}
						if (rotation == 90) {
								return Direction.LEFT;
						}

				}

				if (direction == PlayerController.RIGHT_UP) {
						if (rotation == 180) {
								return Direction.RIGHT;
						}
						if (rotation == 90) {
								return Direction.LEFT;
						}
			
				}

				if (direction == PlayerController.RIGHT_DOWN) {
						if (rotation == 180) {
								return Direction.RIGHT;
						}
						if (rotation == 0) {
								return Direction.LEFT;
						}
			
				}

				if (direction == PlayerController.LEFT) {
						if (rotation == 0) {
								return Direction.RIGHT;
						}
						if (rotation == 270) {
								return Direction.LEFT;
						}
			
				}

				if (direction == PlayerController.LEFT_UP) {
						if (rotation == 0) {
								return Direction.RIGHT;
						}
						if (rotation == 180) {
								return Direction.LEFT;
						}
			
				}
		
				if (direction == PlayerController.LEFT_DOWN) {
						if (rotation == 180) {
								return Direction.RIGHT;
						}
						if (rotation == 0) {
								return Direction.LEFT;
						}
			
				}

				if (direction == PlayerController.UP) {
						if (rotation == 270) {
								return Direction.RIGHT;
						}
						if (rotation == 180) {
								return Direction.LEFT;
						}
			
				}

				if (direction == PlayerController.DOWN) {
						if (rotation == 0) {
								return Direction.LEFT;
						}
						if (rotation == 90) {
								return Direction.RIGHT;
						}
			
				}
				return Direction.STRAIGHT;
		}
}
