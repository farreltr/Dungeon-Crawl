using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
	
		public int size_x = 10;
		public int size_z = 10;
		public float tileSize = 100.0f;

		// Use this for initialization
		void Start ()
		{
				BuildBoard ();
		}
	
	
//		public bool isCorner (int x, int y)
//		{
//				return (y == 0 && x == 0) 
//						|| (y == size_z - 1 && x == 0) 
//						|| (y == 0 && x == size_x - 1) 
//						|| (y == size_z - 1 && x == size_x - 1);
//		}
	
		public void BuildBoard ()
		{
				GameObject[] tiles = Resources.LoadAll<GameObject> ("Tiles/Prefabs/");
				for (int x=0; x < size_x; x++) {
						for (int y=0; y < size_z; y++) {
								Vector3 position = new Vector3 (x * tileSize + (tileSize * 1.5f), y * tileSize + (tileSize * 1.5f), 0);
								GameObject tile = tiles [Random.Range (0, tiles.Length)];
								Quaternion rotation = Quaternion.Euler (0, 0, 90 * (Random.Range (0, 4)));
								tile.tag = string.Concat (x.ToString (), y.ToString ());
								GameObject tileClone = (GameObject)Instantiate (tile, position, rotation);
								tileClone.transform.parent = this.gameObject.transform;
								tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
		
						}
				}
		}

}