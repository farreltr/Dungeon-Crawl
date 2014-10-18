using UnityEngine;
using System.Collections;

public class Board : MonoBehaviour
{
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
				boardTiles = new GameObject[ (TileMap.size_x - 2) * (TileMap.size_y - 2)];
				int i = 0;
				for (int x=0; x < TileMap.size_x - 2; x++) {
						for (int y=0; y < TileMap.size_y - 2; y++) {
								Vector3 position = new Vector3 (x * TileMap.tileSize + (TileMap.tileSize * 1.5f), y * TileMap.tileSize + (TileMap.tileSize * 1.5f), 0);
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
				for (int x=TileMap.size_x-4; x>-1; x--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x + TileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x + 1, y);
			
				}
		
		
		}
	
		public void shiftLeft (int y)
		{
				for (int x=1; x<TileMap.size_x-2; x++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x - TileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x - 1, y);
			
				}
		
		
		}
	
		public void shiftDown (int x)
		{
				for (int y=1; y<TileMap.size_y-2; y++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y - TileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y - 1);
			
				}
		
		
		}
	
		public void shiftUp (int x)
		{
				for (int y=TileMap.size_y -4; y>-1; y--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y + TileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y + 1);
			
				}
		
		
		}
	
		public bool isRight (int x, int y)
		{
				return (x == TileMap.size_x - 1 && y != 0 && y != TileMap.size_y - 1);
		
		}
	
		public bool isLeft (int x, int y)
		{
				return (x == 0 && y != 0 && y != TileMap.size_y - 1);
		
		}
	
		public bool isTop (int x, int y)
		{
				return (y == TileMap.size_y - 1 && x != 0 && x != TileMap.size_x - 1);
		
		}
	
		public bool isBottom (int x, int y)
		{
				return (y == 0 && x != 0 && x != TileMap.size_x - 1);
		
		}


}