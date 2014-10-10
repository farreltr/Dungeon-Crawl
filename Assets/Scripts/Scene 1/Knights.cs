using UnityEngine;
using System.Collections;

public class Knights : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				GameObject[] knights = Resources.LoadAll<GameObject> ("Knights/Prefabs/");
				foreach (GameObject knight in knights) {
						GameObject s = (GameObject)Instantiate (knight);
						s.transform.parent = this.transform;
						BoxCollider2D collider = s.AddComponent<BoxCollider2D> ();
						Rigidbody2D rb = s.AddComponent<Rigidbody2D> ();
						rb.gravityScale = 0.0f;
						rb.fixedAngle = true;
						rb.angularDrag = 0.0f;
						rb.isKinematic = false;
				}
	
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
