using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
	
		public int size_x = 8;
		public int size_y = 8;
		public float tileSize = 100.0f;
		public static Board board;
		public GameObject[] boardTiles;

		void Awake ()
		{
				if (board == null) {
						DontDestroyOnLoad (board);
						board = this;
				} else if (board != this) {
						Destroy (gameObject);
				}
		}

		// Use this for initialization
		void Start ()
		{
				DontDestroyOnLoad (gameObject);
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
				boardTiles = new GameObject[size_x * size_y];
				int i = 0;
				for (int x=0; x < size_x; x++) {
						for (int y=0; y < size_y; y++) {
								Vector3 position = new Vector3 (x * tileSize + (tileSize * 1.5f), y * tileSize + (tileSize * 1.5f), 0);
								GameObject tile = tiles [Random.Range (0, tiles.Length)];
								Quaternion rotation = Quaternion.Euler (0, 0, 90 * (Random.Range (0, 4)));
								tile.tag = string.Concat (x.ToString (), y.ToString ());
								GameObject tileClone = (GameObject)Instantiate (tile, position, rotation);
								tileClone.transform.parent = this.gameObject.transform;
								tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
								boardTiles [i] = tileClone;
								i++;
						}
				}
		}

		public void shiftRight (int y)
		{
				for (int x=board.size_x-2; x>-1; x--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x + TileMap.tileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x + 1, y);
			
				}
		
		
		}
	
		public void shiftLeft (int y)
		{
				for (int x=1; x<board.size_x; x++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x - TileMap.tileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x - 1, y);
			
				}
		
		
		}
	
		public void shiftDown (int x)
		{
				for (int y=1; y<board.size_y; y++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y - TileMap.tileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y - 1);
			
				}
		
		
		}
	
		public void shiftUp (int x)
		{
				for (int y=board.size_y -2; y>-1; y--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y + TileMap.tileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y + 1);
			
				}
		
		
		}
	
		public bool isRight (int x, int y)
		{
				return (x == TileMap.tileMap.size_x - 1 && y != 0 && y != TileMap.tileMap.size_y - 1);
		
		}
	
		public bool isLeft (int x, int y)
		{
				return (x == 0 && y != 0 && y != TileMap.tileMap.size_y - 1);
		
		}
	
		public bool isTop (int x, int y)
		{
				return (y == TileMap.tileMap.size_y - 1 && x != 0 && x != TileMap.tileMap.size_x - 1);
		
		}
	
		public bool isBottom (int x, int y)
		{
				return (y == 0 && x != 0 && x != TileMap.tileMap.size_x - 1);
		
		}


}