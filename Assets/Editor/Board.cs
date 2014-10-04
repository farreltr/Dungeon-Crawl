using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Board : MonoBehaviour
{
	
		public int size_x = 10;
		public int size_z = 10;
		public float tileSize = 100.0f;
		public Tile[,] board;
		public Vector2 position;
	
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
				BuildBoard ();
		}
	
	
		public bool isCorner (int x, int y)
		{
				return (y == 0 && x == 0) 
						|| (y == size_z - 1 && x == 0) 
						|| (y == 0 && x == size_x - 1) 
						|| (y == size_z - 1 && x == size_x - 1);
		}
	
		public void BuildBoard ()
		{
				board = new Tile[size_x, size_z];
				Transform[] tiles = Resources.LoadAll<Transform> ("Tiles/Prefabs/");
				for (int y=0; y < size_z; y++) {
						for (int x=0; x < size_x; x++) {
								Vector3 position = new Vector3 (x * tileSize + (tileSize * 1.5f), 0.5f, y * tileSize + (tileSize * 1.5f));
								Transform tile = isCorner (x, y) ? Resources.Load<Transform> ("Tiles/Prefabs/cross-junction") : tiles [Random.Range (0, tiles.Length)];
								Quaternion rotation = Quaternion.Euler (90, 0, 90 * (Random.Range (0, 3)));
								tile.tag = string.Concat (x.ToString (), y.ToString ());
								Instantiate (tile, position, rotation);
						}
				}
		
		}
	
}
