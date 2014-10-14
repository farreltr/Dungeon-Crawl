using UnityEngine;
using System.Collections;

public class Arrows : MonoBehaviour
{
	
		public int size_x = 12;
		public int size_z = 12;
		public float tileSize = 100.0f;
		private GameObject sword;
		public static Arrows arrows;
	
		void Awake ()
		{
				if (arrows == null) {
						DontDestroyOnLoad (arrows);
						arrows = this;
				} else if (arrows != this) {
						Destroy (gameObject);
				}
		}

	
		enum Direction
		{
				Up = 1,
				Down = 3,
				Left = 2,
				Right = 0
		}
	
		// Use this for initialization
		void Start ()
		{
				DontDestroyOnLoad (gameObject);
				sword = Resources.Load<GameObject> ("Arrows/sword");
				SpriteRenderer renderer = sword.GetComponent<SpriteRenderer> ();
				renderer.sortingLayerID = 5;
				renderer.sortingOrder = 5;
				RightArrows ();
				LeftArrows ();
				UpArrows ();
				DownArrows ();
		}
	
		void RightArrows ()
		{
				//Arrows pointing right
				for (int y = 1; y < size_z - 1; y++) {
						Vector3 position = new Vector3 (tileSize * 0.5f, y * tileSize + (tileSize * 0.5f), 0);
						Quaternion rotation = Quaternion.Euler (0, 0, 270);
						GameObject swordClone = (GameObject)Instantiate (sword, position, rotation);
						swordClone.transform.tag = "Right Arrow";
						swordClone.layer = this.gameObject.layer;
						swordClone.name = swordClone.transform.tag;
						swordClone.transform.parent = this.gameObject.transform;
				}
		}

		void LeftArrows ()
		{
				//Arrows pointing right
				for (int y = 1; y < size_z - 1; y++) {
						Vector3 position = new Vector3 ((size_x - 1) * tileSize + (tileSize * 0.5f), y * tileSize + (tileSize * 0.5f), 0);
						Quaternion rotation = Quaternion.Euler (0, 0, 90);
						GameObject swordClone = (GameObject)Instantiate (sword, position, rotation);
						swordClone.transform.tag = "Left Arrow";
						swordClone.layer = this.gameObject.layer;
						swordClone.name = swordClone.transform.tag;
						swordClone.transform.parent = this.gameObject.transform;
				}
		}

		void UpArrows ()
		{
				//Arrows pointing right
				for (int x = 1; x < size_x - 1; x++) {
						Vector3 position = new Vector3 (x * tileSize + tileSize * 0.5f, 0 * tileSize + (tileSize * 0.5f), 0);
						Quaternion rotation = Quaternion.Euler (0, 0, 0);
						GameObject swordClone = (GameObject)Instantiate (sword, position, rotation);
						swordClone.transform.tag = "Up Arrow";
						swordClone.layer = this.gameObject.layer;
						swordClone.name = swordClone.transform.tag;
						swordClone.transform.parent = this.gameObject.transform;
				}
		}

		void DownArrows ()
		{
				//Arrows pointing right
				for (int x = 1; x < size_x - 1; x++) {
						Vector3 position = new Vector3 (x * tileSize + tileSize * 0.5f, (size_z - 1) * tileSize + (tileSize * 0.5f), 0);
						Quaternion rotation = Quaternion.Euler (0, 0, 180);
						GameObject swordClone = (GameObject)Instantiate (sword, position, rotation);
						swordClone.transform.tag = "Right Arrow";
						swordClone.layer = this.gameObject.layer;
						swordClone.name = swordClone.transform.tag;
						swordClone.transform.parent = this.gameObject.transform;
				}
		}

}