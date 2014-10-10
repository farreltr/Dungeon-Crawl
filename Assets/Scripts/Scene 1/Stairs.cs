using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				GameObject[] stairs = Resources.LoadAll<GameObject> ("Stairs/Prefabs/");
				foreach (GameObject staircase in stairs) {
						GameObject s = (GameObject)Instantiate (staircase);
						s.transform.parent = this.transform;
						SpriteRenderer renderer = s.GetComponent<SpriteRenderer> ();
						renderer.sortingLayerID = 6;
						renderer.sortingOrder = 0;
						s.AddComponent<StairsCollider> ();
						BoxCollider2D collider = s.AddComponent<BoxCollider2D> ();
						collider.size = new Vector2 (1.0f, 0.5f);
						collider.center = new Vector2 (0.0f, -0.25f);
						Rigidbody2D rigidbody = s.AddComponent<Rigidbody2D> ();
						rigidbody.gravityScale = 0.0f;
				}
		
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
	
}
