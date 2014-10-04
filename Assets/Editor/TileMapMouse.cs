using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TileMap))]
[RequireComponent(typeof(Inventory))]
[RequireComponent(typeof(Board))]
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
				inventory = GetComponent<Inventory> ();
				board = GetComponent<Board> ();
		}

		// Update is called once per frame
		void Update ()
		{
				Ray ray = Camera.mainCamera.ScreenPointToRay (Input.mousePosition);
				RaycastHit hitInfo;
		
				if (collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
						int x = Mathf.FloorToInt (hitInfo.point.x / tileMap.tileSize);
						int z = Mathf.FloorToInt (hitInfo.point.z / tileMap.tileSize);
						Debug.Log ("Tile: " + x + ", " + z);
			
						currentTileCoord.x = x;
						currentTileCoord.z = z;

						if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
								Debug.Log ("Tile: " + x + ", " + z);
						}
			
						selectionCube.transform.position = currentTileCoord * 5f;
				} else {
						// Hide selection cube?
				}
		
				if (Input.GetMouseButtonDown (0)) {
						Debug.Log ("Click!");
				}
		}
}
