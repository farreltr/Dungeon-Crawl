using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
		public Inventory inventory;
		public Vector3 currentTileCoord;	
		private bool restart;
		private bool gameOver;
		private static string EMPTY_STRING = "";
		private static int nextPlayer = 1;
		public int numberOfPLayers = 1;

		public static GameController controller;
	
		void Awake ()
		{
				if (controller == null) {
						DontDestroyOnLoad (controller);
						controller = this;
				} else if (controller != this) {
						Destroy (gameObject);
				}
		}
	
		void Start ()
		{
				DontDestroyOnLoad (gameObject);
				gameOver = false;
				restart = false;
				//restartText.text = EMPTY_STRING;
				//gameOverText.text = EMPTY_STRING;
				inventory = GameObject.FindObjectOfType<Inventory> ();
				
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
						//Load Inventory for next player. Do this from a file later
						inventory = GameObject.FindObjectOfType<Inventory> ();
		
						if (TileMap.tileMap.collider.Raycast (ray, out hitInfo, Mathf.Infinity)) {
								int x = Mathf.FloorToInt (hitInfo.point.x / TileMap.tileSize);
								int y = Mathf.FloorToInt (hitInfo.point.y / TileMap.tileSize);
								//Debug.Log ("Tile: " + x + ", " + y);
			
								currentTileCoord.x = x;
								currentTileCoord.y = y;

								string tileIdx = EMPTY_STRING;

								if (inventory.e != null && inventory.e.type == EventType.mouseUp && inventory.draggingTile) {
										if (Board.board.isLeft (x, y)) {
												putTileBackInHand (x + (TileMap.size_x - 2) - 1, y - 1);
												Knights.knights.ShiftKnightsRight (x + (TileMap.size_x - 2) - 1, y - 1);
												Board.board.shiftRight (y - 1);
												InstantiateDraggedTile (new Vector3 (TileMap.tileSize * (x + 1.5f), TileMap.tileSize * (y + 0.5f), 0.5f), string.Concat (x, y - 1));											
												GoToNextPlayer ();
										} else if (Board.board.isRight (x, y)) {
												putTileBackInHand (x - (TileMap.size_x - 2) - 1, y - 1);
												Knights.knights.ShiftKnightsLeft (x - (TileMap.size_x - 2) - 1, y - 1);			
												Board.board.shiftLeft (y - 1);											
												InstantiateDraggedTile (new Vector3 (TileMap.tileSize * (x - 0.5f), TileMap.tileSize * (y + 0.5f), 0.5f), string.Concat (x - 2, y - 1));
												GoToNextPlayer ();
										} else if (Board.board.isTop (x, y)) {
												putTileBackInHand (x - 1, y - (TileMap.size_y - 2) - 1);
												Knights.knights.ShiftKnightsDown (x - 1, y - (TileMap.size_y - 2) - 1);
												Board.board.shiftDown (x - 1);											
												InstantiateDraggedTile (new Vector3 (TileMap.tileSize * (x + 0.5f), TileMap.tileSize * (y - 0.5f), 0.5f), string.Concat (x - 1, y - 2));
												GoToNextPlayer ();
										} else if (Board.board.isBottom (x, y)) {
												putTileBackInHand (x - 1, y + (TileMap.size_y - 2) - 1);
												Knights.knights.ShiftKnightsUp (x - 1, y + (TileMap.size_y - 2) - 1);
												Board.board.shiftUp (x - 1);
												InstantiateDraggedTile (new Vector3 (TileMap.tileSize * (x + 0.5f), TileMap.tileSize * (y + 1.5f), 0.5f), string.Concat (x - 1, y));								
												GoToNextPlayer ();
										} 								
								}
						}
				}
		}

		void GoToNextPlayer ()
		{
				nextPlayer++;
				if (nextPlayer == numberOfPLayers + 1) {
						nextPlayer = 1;
				}
				Application.LoadLevel (nextPlayer);
				inventory.Save ();

		}

		void InstantiateDraggedTile (Vector3 position, string tag)
		{
				GameObject tile = Resources.Load<GameObject> ("Tiles/Prefabs/" + inventory.draggedTile.name);
				GameObject tileClone = (GameObject)Instantiate (tile, position, inventory.draggedTile.gameObject.transform.rotation);
				tileClone.transform.parent = Board.board.transform;
				tileClone.transform.GetComponent<SpriteRenderer> ().sortingLayerName = "Board Tile";
				inventory.draggingTile = false;
				tileClone.tag = tag;
		}

		void CheckRestart ()
		{
				if (restart && inventory.isRestart) {
						Application.LoadLevel (Application.loadedLevel);
				}
		}

		void putTileBackInHand (int x, int y)
		{
				GameObject go = GameObject.FindGameObjectWithTag (string.Concat (x, y));
				string cloneString = go.transform.name;
				GameObject newTile = new GameObject ();
				newTile.transform.rotation = go.transform.rotation;
				Tile tile = newTile.AddComponent<Tile> ();
				tile.SetUpTile (Tile.getTileType (cloneString.Replace ("(Clone)", "")));
				inventory.inventory [inventory.prevIdx] = tile;
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
										GameObject.FindObjectOfType<Inventory> ().SetDisabled ();							

								}
						}

						if (animator != null) {
								animator.enabled = false;
						}
						restart = true;
				}

		}
}
