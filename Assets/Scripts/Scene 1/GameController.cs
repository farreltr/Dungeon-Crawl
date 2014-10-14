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
				tileMap = TileMap.tileMap;
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<Inventory> ();
				board = Board.board;
				
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
										if (board.isLeft (x, y)) {
												putTileBackInHand (x + board.size_x - 1, y - 1);
												Knights.knights.ShiftKnightsRight (x + board.size_x - 1, y - 1);
												board.shiftRight (y - 1);
												InstantiateDraggedTile (new Vector3 (tileMap.tileSize * (x + 1.5f), tileMap.tileSize * (y + 0.5f), 0.5f), string.Concat (x, y - 1));											
										} else if (board.isRight (x, y)) {
												putTileBackInHand (x - board.size_x - 1, y - 1);
												Knights.knights.ShiftKnightsLeft (x - board.size_x - 1, y - 1);			
												board.shiftLeft (y - 1);											
												InstantiateDraggedTile (new Vector3 (tileMap.tileSize * (x - 0.5f), tileMap.tileSize * (y + 0.5f), 0.5f), string.Concat (x - 2, y - 1));
										} else if (board.isTop (x, y)) {
												putTileBackInHand (x - 1, y - board.size_z - 1);
												Knights.knights.ShiftKnightsDown (x - 1, y - board.size_z - 1);
												board.shiftDown (x - 1);											
												InstantiateDraggedTile (new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y - 0.5f), 0.5f), string.Concat (x - 1, y - 2));
										} else if (board.isBottom (x, y)) {
												putTileBackInHand (x - 1, y + board.size_z - 1);
												Knights.knights.ShiftKnightsUp (x - 1, y + board.size_z - 1);
												board.shiftUp (x - 1);
												InstantiateDraggedTile (new Vector3 (tileMap.tileSize * (x + 0.5f), tileMap.tileSize * (y + 1.5f), 0.5f), string.Concat (x - 1, y));								
										} 								
								}
						}
				}
		}

		void InstantiateDraggedTile (Vector3 position, string tag)
		{
				GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
				GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.rotation);
				tileClone.transform.parent = this.board.transform;
				tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
				inventory.draggingTile = false;
				tileClone.tag = tag;
		}

		void CheckRestart ()
		{
				if (restart) {
						if (Input.GetKeyDown (KeyCode.R)) {
								Application.LoadLevel (Application.loadedLevel);
						}
				}
		}

		void putTileBackInHand (int x, int y)
		{
				GameObject go = GameObject.FindGameObjectWithTag (string.Concat (x, y));
				string cloneString = go.transform.name;
				GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + cloneString.Replace ("(Clone)", ""));
				tile.transform.rotation = go.transform.rotation;			
				inventory.inventory [inventory.prevIdx] = new Tile (tile);
				Destroy (go);
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
