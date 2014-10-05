using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
public class TileMapMouse : MonoBehaviour
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
				Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
		
				if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
						int x = Mathf.FloorToInt (hitInfo.point.x / tileMap.tileSize);
						int z = Mathf.FloorToInt (hitInfo.point.z / tileMap.tileSize);
						//Debug.Log ("Tile: " + x + ", " + z);
			
						currentTileCoord.x = x;
						currentTileCoord.z = z;

						if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
								Vector3 position = new Vector3 (tileMap.tileSize * (x + 0.5f), 0.5f, tileMap.tileSize * (z + 0.5f));
								GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
								Instantiate (tile, position, Quaternion.Euler (new Vector3 (90, 0, 0)));
								//inventory.inventory [inventory.prevIdx] = board.board [tileMap.size_x - 3, z - 1]; // put last tile in hand
								inventory.draggingTile = false;
								GameObject[] go = GameObject.FindGameObjectsWithTag (string.Concat (z - 1, x + 9));
								if (go.Length == 1) {
										inventory.inventory [inventory.prevIdx] = new Tile (go [0].transform);
										Destroy (go [0]);
								} else {
										Debug.Log (string.Concat (z - 1, x + 9));
										Debug.LogError ("Something went wrong");
								}

								
						}
			
						//selectionCube.transform.position = currentTileCoord * 5f;
				} else {
						// Hide selection cube?
				}
		
				//if (Input.GetMouseButtonDown (0)) {
				//	Debug.Log ("Click!");
				//}
		}
}
