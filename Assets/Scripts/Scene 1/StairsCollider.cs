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
				string colliderName = colliderObject.name.Replace ("(Clone)", "");
				string myName = this.gameObject.name.Replace ("(Clone)", "");
				if (myName == colliderName) {
						controller.GameOver ();
				} else {
						PlayerController controller = colliderObject.transform.GetComponent<PlayerController> ();
						if (controller != null) {
								controller.ChangeDirection ();
						}
				
				}
		
		}

		bool isKnight (string colour)
		{
				return colour == "red" || colour == "blue" || colour == "green" || colour == "yellow";
		}
}
