using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
		TileMap tileMap;
		Inventory inventory;
		Board board;	
		Vector3 currentTileCoord;	
		public Transform selectionCube;
	
		void Start ()
		{
				tileMap = GameObject.FindGameObjectWithTag ("Tile Map").GetComponent<TileMap> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				board = GameObject.FindGameObjectWithTag ("Board").GetComponent<Board> ();
		}

		// Update is called once per frame
		void Update ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
		
				if (tileMap.collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
						int x = Mathf.FloorToInt (hitInfo.point.x / tileMap.tileSize);
						int y = Mathf.FloorToInt (hitInfo.point.y / tileMap.tileSize);
						//Debug.Log ("Tile: " + x + ", " + y);
			
						currentTileCoord.x = x;
						currentTileCoord.y = y;
						print ("x: " + x + ", y : " + y);

						if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
								if (isLeft (x, y)) {
										// create tile index string
										putTileBackInHand (string.Concat (x + board.size_x - 1, y - 1));
										shiftRight (y - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), tileMap.tileSize * (y + 0.5f), 0.5f);
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x, y - 1);
										Instantiate (tile, position, inventory.draggedTile.rotation);
										inventory.draggingTile = false;
										inventory.draggedTile = null;
								} else if (isRight (x, y)) {
										// shiftLeft();
										putTileBackInHand (string.Concat (x - board.size_x - 1, y - 1));
										shiftLeft (y - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x - 0.5f), tileMap.tileSize * (y + 0.5f), 0.5f);
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x - 2, y - 1);
										Instantiate (tile, position, inventory.draggedTile.rotation);
										inventory.draggingTile = false;
								} else if (isTop (x, y)) {
										putTileBackInHand (string.Concat (x - 1, y - board.size_z - 1));
										shiftDown (x - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y - 0.5f), 0.5f);
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x - 1, y - 2);
										Instantiate (tile, position, inventory.draggedTile.rotation);
										inventory.draggingTile = false;
										//shiftDown();
								} else if (isBottom (x, y)) {
										//shiftUp();
										putTileBackInHand (string.Concat (x - 1, y + board.size_z - 1));
										shiftUp (x - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y + 1.5f), 0.5f);
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);									
										tile.tag = string.Concat (x - 1, y);
										Instantiate (tile, position, inventory.draggedTile.rotation);
										inventory.draggingTile = false;
					
								} 

								
						}
				}
		}

		void putTileBackInHand (string tileIdx)
		{
				GameObject[] go = GameObject.FindGameObjectsWithTag (tileIdx);
				if (go.Length == 1) {
						string cloneString = go [0].transform.name;
						GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + cloneString.Replace ("(Clone)", ""));
						tile.transform.rotation = go [0].transform.rotation;			
						inventory.inventory [inventory.prevIdx] = new Tile (tile);
						Destroy (go [0]);
				} else {
						Debug.Log (tileIdx);
						Debug.LogError ("Something went wrong");
				}
		}

		void shiftRight (int y)
		{
				for (int x=board.size_x-2; x>-1; x--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x + tileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x + 1, y);

				}

			
		}

		void shiftLeft (int y)
		{
				for (int x=1; x<board.size_x; x++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x - tileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x - 1, y);
			
				}
		
		
		}

		void shiftDown (int x)
		{
				for (int y=1; y<board.size_z; y++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y - tileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y - 1);
			
				}
		
		
		}

		void shiftUp (int x)
		{
				for (int y=board.size_z -2; y>-1; y--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y + tileMap.tileSize, position.z);
						tile.tag = string.Concat (x, y + 1);

				}
		
		
		}

		bool isRight (int x, int y)
		{
				return (x == tileMap.size_x - 1 && y != 0 && y != tileMap.size_z - 1);
		
		}

		bool isLeft (int x, int y)
		{
				return (x == 0 && y != 0 && y != tileMap.size_z - 1);

		}

		bool isTop (int x, int y)
		{
				return (y == tileMap.size_z - 1 && x != 0 && x != tileMap.size_x - 1);

		}

		bool isBottom (int x, int y)
		{
				return (y == 0 && x != 0 && x != tileMap.size_x - 1);
		
		}
}
