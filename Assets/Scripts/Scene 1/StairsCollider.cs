using UnityEngine;
using System.Collections;

public class StairsCollider : MonoBehaviour
{

		public GameController controller;

		// Use this for initialization
		void Start ()
		{
				controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}

		void OnTriggerEnter2D (Collider2D collider)
		{
				GameObject colliderObject = collider.attachedRigidbody.gameObject;
				string colliderName = GetFormattedName (colliderObject);
				string myName = GetFormattedName (gameObject);
				PlayerController playerController = colliderObject.transform.GetComponent<PlayerController> ();
				if (myName == colliderName) {
						playerController.isWinner = true;
						controller.GameOver ();
				} else {
						if (playerController != null) {
								playerController.ChangeDirection ();
						}
				
				}
		
		}

		private static string GetFormattedName (GameObject o)
		{
				return o.name.Replace ("(Clone)", "");
		}

		bool isKnight (string colour)
		{
				return colour == "red" || colour == "blue" || colour == "green" || colour == "yellow";
		}
}
