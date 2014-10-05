using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class GameController : MonoBehaviour
{
	
		TileMap tileMap;
		Inventory inventory;
		Board board;	
		Vector3 currentTileCoord;	
		public Transform selectionCube;
	
		void Start ()
		{
				tileMap = GetComponent<TileMap> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				board = GameObject.FindGameObjectWithTag ("Board").GetComponent<Board> ();
		}

		// Update is called once per frame
		void Update ()
		{
				Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
		
				if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
						int x = Mathf.FloorToInt (hitInfo.point.x / tileMap.tileSize);
						int z = Mathf.FloorToInt (hitInfo.point.z / tileMap.tileSize);
						//Debug.Log ("Tile: " + x + ", " + z);
			
						currentTileCoord.x = x;
						currentTileCoord.z = z;

						if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
								if (isLeft (x, z)) {
										// create tile index string
										putTileBackInHand (string.Concat (x + 9, z - 1));
										shiftRight (z - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), 0.5f, tileMap.tileSize * (z + 0.5f));
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x, z - 1);
										Instantiate (tile, position, Quaternion.Euler (new Vector3 (90, 0, 0)));
										inventory.draggingTile = false;
										inventory.draggedTile = null;
								} else if (isRight ()) {
										// shiftLeft();
										putTileBackInHand (string.Concat (x + 9, z - 1));
										shiftLeft (z - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), 0.5f, tileMap.tileSize * (z + 0.5f));
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x, z - 1);
										Instantiate (tile, position, Quaternion.Euler (new Vector3 (90, 0, 0)));
										inventory.draggingTile = false;
								} else if (isTop ()) {
										putTileBackInHand (string.Concat (x + 9, z - 1));
										shiftDown (x - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), 0.5f, tileMap.tileSize * (z + 0.5f));
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x, z - 1);
										Instantiate (tile, position, Quaternion.Euler (new Vector3 (90, 0, 0)));
										inventory.draggingTile = false;
										//shiftDown();
								} else if (isBottom ()) {
										//shiftUp();
										putTileBackInHand (string.Concat (x + 9, z - 1));
										shiftUp (x - 1);
										Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), 0.5f, tileMap.tileSize * (z + 0.5f));
										GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
										tile.tag = string.Concat (x, z - 1);
										Instantiate (tile, position, Quaternion.Euler (new Vector3 (90, 0, 0)));
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
						Transform tile = Resources.Load<Transform> ("Tiles/Prefabs/" + cloneString.Replace ("(Clone)", ""));
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
				for (int x=0; x>board.size_x-1; x++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x - tileMap.tileSize, position.y, position.z);
						tile.tag = string.Concat (x - 1, y);
			
				}
		
		
		}

		void shiftDown (int x)
		{
				for (int y=board.size_z-2; y>-1; y--) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y, position.z + tileMap.tileSize);
						tile.tag = string.Concat (x, y + 1);
			
				}
		
		
		}

		void shiftUp (int x)
		{
				for (int y=0; y<board.size_z; y++) {
						GameObject tile = GameObject.FindGameObjectWithTag (string.Concat (x, y));
						Vector3 position = tile.transform.position;
						tile.transform.position = new Vector3 (position.x, position.y, position.z - tileMap.tileSize);
						tile.tag = string.Concat (x, y - 1);
			
				}
		
		
		}

		bool isRight ()
		{
				return false;

		}

		bool isLeft (int x, int y)
		{
				return (x == 0 && y != 0 && y != tileMap.size_z - 1);

		}

		bool isTop ()
		{
				return false;

		}

		bool isBottom ()
		{
				return false;

		}
}
