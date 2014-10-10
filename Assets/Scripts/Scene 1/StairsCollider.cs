using UnityEngine;
using System.Collections;

public class StairsCollider : MonoBehaviour
{

		public GameController controller;

		// Use this for initialization
		void Start ()
		{
				controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
				BoxCollider2D boxCollider = transform.gameObject.AddComponent<BoxCollider2D> ();	
				boxCollider.size = new Vector2 (1.0f, 0.5f);
				boxCollider.center = new Vector2 (0.0f, -0.25f);
				boxCollider.isTrigger = true;
	
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
						if (isKnight (colliderName)) {
								collider.transform.GetComponent<PlayerController> ().ChangeDirection ();
						}
				
				}
		
		}

		bool isKnight (string colour)
		{
				return colour == "red" || colour == "blue" || colour == "green" || colour == "yellow";
		}
}
