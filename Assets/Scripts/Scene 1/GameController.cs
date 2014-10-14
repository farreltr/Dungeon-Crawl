using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	
		TileMap tileMap;
		Inventory inventory;
		Board board;	
		Vector3 currentTileCoord;	
		private bool restart;
		private bool gameOver;
		public GUIText restartText;
		public GUIText gameOverText;

		private static string EMPTY_STRING = "";
	
		void Start ()
		{
				gameOver = false;
				restart = false;
				restartText.text = EMPTY_STRING;
				gameOverText.text = EMPTY_STRING;
				tileMap = GameObject.FindGameObjectWithTag ("Tile Map").GetComponent<TileMap> ();
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				board = GameObject.FindGameObjectWithTag ("Board").GetComponent<Board> ();
				
		}

		// Update is called once per frame
		void Update ()
		{
				if (gameOver) {
						CheckRestart ();
						//display winner
						//destroy all objects

				} else {
						


						Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
						RaycastHit hitInfo;
		
						if (tileMap.collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
								int x = Mathf.FloorToInt (hitInfo.point.x / tileMap.tileSize);
								int y = Mathf.FloorToInt (hitInfo.point.y / tileMap.tileSize);
								//Debug.Log ("Tile: " + x + ", " + y);
			
								currentTileCoord.x = x;
								currentTileCoord.y = y;

								string tileIdx = EMPTY_STRING;

								if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
										if (isLeft (x, y)) {
												tileIdx = string.Concat (x + board.size_x - 1, y - 1);
												putTileBackInHand (tileIdx);
												foreach (PlayerController knight in GameObject.FindObjectsOfType<PlayerController>()) {
														if (knight.isOnRow (y)) {
																if (knight.checkRespawn (tileIdx)) {
																		knight.respawn ();
																} else {
																		if (!knight.isRespawn) {
																				knight.transform.position = new Vector3 (knight.GetPosition ().x + 100.0f, knight.GetPosition ().y, 0.0f);
																		}
																		
																}
								
														}
							
												}
												shiftRight (y - 1);
												Vector3 position = new Vector3 (tileMap.tileSize * (x + 1.5f), tileMap.tileSize * (y + 0.5f), 0.5f);
												GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
												tile.tag = string.Concat (x, y - 1);												
												GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.rotation);
												tileClone.transform.parent = this.board.transform;
												tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
												inventory.draggingTile = false;
										} else if (isRight (x, y)) {
												tileIdx = string.Concat (x - board.size_x - 1, y - 1);
												putTileBackInHand (tileIdx);
												foreach (PlayerController knight in GameObject.FindObjectsOfType<PlayerController>()) {
														if (knight.isOnRow (y)) {
																if (knight.checkRespawn (tileIdx)) {
																		knight.respawn ();
																} else {
																		if (!knight.isRespawn) {
																				knight.transform.position = new Vector3 (knight.GetPosition ().x - 100.0f, knight.GetPosition ().y, 0.0f);
																		}
																}
														}
							
												}			
												shiftLeft (y - 1);
												Vector3 position = new Vector3 (tileMap.tileSize * (x - 0.5f), tileMap.tileSize * (y + 0.5f), 0.5f);
												GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
												tile.tag = string.Concat (x - 2, y - 1);
												GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.rotation);
												tileClone.transform.parent = this.board.transform;
												tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
												inventory.draggingTile = false;
										} else if (isTop (x, y)) {
												tileIdx = string.Concat (x - 1, y - board.size_z - 1);
												putTileBackInHand (tileIdx);
												foreach (PlayerController knight in GameObject.FindObjectsOfType<PlayerController>()) {
														if (knight.isOnColumn (x)) {
																if (knight.checkRespawn (tileIdx)) {
																		knight.respawn ();
																} else {
																		if (!knight.isRespawn) {
																				knight.transform.position = new Vector3 (knight.GetPosition ().x, knight.GetPosition ().y - 100.0f, 0.0f);
																		}
																}
														}
												}
												shiftDown (x - 1);
												Vector3 position = new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y - 0.5f), 0.5f);
												GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
												tile.tag = string.Concat (x - 1, y - 2);
												GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.rotation);
												tileClone.transform.parent = this.board.transform;
												tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
												inventory.draggingTile = false;
										} else if (isBottom (x, y)) {
												tileIdx = string.Concat (x - 1, y + board.size_z - 1);
												putTileBackInHand (tileIdx);
												foreach (PlayerController knight in GameObject.FindObjectsOfType<PlayerController>()) {
														if (knight.isOnColumn (x)) {
																if (knight.checkRespawn (tileIdx)) {
																		knight.respawn ();
																} else {
																		if (!knight.isRespawn) {
																				knight.transform.position = new Vector3 (knight.GetPosition ().x, knight.GetPosition ().y + 100.0f, 0.0f);
																		}
																}
														}							
												}
												shiftUp (x - 1);
												Vector3 position = new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y + 1.5f), 0.5f);
												GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);									
												tile.tag = string.Concat (x - 1, y);
												GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.rotation);
												tileClone.transform.parent = this.board.transform;
												tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
												inventory.draggingTile = false;					
										} 								
								}
						}
				}
		}

		void CheckRestart ()
		{
				if (restart) {
						if (Input.GetKeyDown (KeyCode.R)) {
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}

		void putTileBackInHand (string tileIdx)
		{
				GameObject go = GameObject.FindGameObjectWithTag (tileIdx);
				string cloneString = go.transform.name;
				GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + cloneString.Replace ("(Clone)", ""));
				tile.transform.rotation = go.transform.rotation;			
				inventory.inventory [inventory.prevIdx] = new Tile (tile);
				Destroy (go);
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
				return (x == tileMap.size_x - 1 && y != 0 && y != tileMap.size_y - 1);
		
		}

		bool isLeft (int x, int y)
		{
				return (x == 0 && y != 0 && y != tileMap.size_y - 1);

		}

		bool isTop (int x, int y)
		{
				return (y == tileMap.size_y - 1 && x != 0 && x != tileMap.size_x - 1);

		}

		bool isBottom (int x, int y)
		{
				return (y == 0 && x != 0 && x != tileMap.size_x - 1);
		
		}

		public void GameOver ()
		{
				//gameOverText.text = "Game Over!";
				gameOver = true;
				foreach (GameObject player in GameObject.FindGameObjectsWithTag ("Player")) {
						PlayerController controller = player.GetComponent<PlayerController> ();
						Animator animator = player.GetComponent<Animator> ();	
						if (controller != null) {
								controller.Stop ();
								if (controller.isWinner) {
										//gameOverText.text = controller.GetName () + " wins!";
										Instantiate (Resources.Load<GUITexture> ("End Screens/" + controller.GetName ()));
										GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ().SetDisabled ();									

								}
						}

						if (animator != null) {
								animator.enabled = false;
						}
						restart = true;
				}

		}
}
