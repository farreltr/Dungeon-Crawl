using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
		public List<Tile> inventory = new List<Tile> ();
		public List<Tile> slots = new List<Tile> ();
		private TileDB database;
		public int slotsX, slotsY;
		private bool showInventory;
		public GUISkin skin;
		public Vector2 position;
	
		public Vector3 currentTileCoord;
	
		public static string EMPTY_STRING = "";
	
		public bool showToolTip;
		public string tooltip;
		public bool draggingTile;
		public Tile draggedTile;
		public int prevIdx;
		public Event e;
		private float tileWidth = 37.0f;
		private float tileHeight = 37.0f;
		private bool disabled = false;
	
		// Use this for initialization
		void Start ()
		{
				for (int i=0; i<(slotsX*slotsY); i++) {
						slots.Add (new Tile ());
						inventory.Add (new Tile ());
			
				}
				database = GameObject.FindGameObjectWithTag ("Tile Database").GetComponent<TileDB> ();
				for (int i = 0; i<inventory.Count; i++) {
						int tileId = Random.Range (0, database.tiles.Count - 1);
						AddTile (tileId);
			
				}
		
		}

//		GameObject CreateNewTile ()
//		{
//				GameObject tile = new GameObject ();
//		}
	
		// Update is called once per frame
		void Update ()
		{
		
		}
	
		void OnGUI ()
		{
				if (!disabled) {
						DrawInventory ();
				}


		
		}
	
		public void DrawInventory ()
		{
				tooltip = EMPTY_STRING;
				GUI.skin = skin;
				//Rect boxRect = new Rect ((4 * Screen.width / 5) - 10, (1 * Screen.height / 6) - 10, tileWidth * 2f, tileHeight * 4.5f);
				//GUI.Box (boxRect, EMPTY_STRING);
		
				e = Event.current;
				int i = 0;
				for (int y=0; y<slotsY; y++) {
						for (int x=0; x<slotsX; x++) {
								Rect slotRect = new Rect (x * tileWidth * 1.1f + (4 * Screen.width / 5), y * (tileHeight + 10) * 1.1f + (1 * Screen.height / 6), tileWidth * 1.1f, tileHeight * 1.1f);
								Rect tileRect = new Rect (slotRect.x + (tileWidth * 0.05f), slotRect.y + (tileHeight * 0.05f), tileWidth, tileHeight);
								Rect rotRightRect = new Rect (slotRect.x - (0.4f * tileWidth), slotRect.y + (0.6f * tileHeight), slotRect.width * 0.4f, slotRect.height * 0.4f);
								Rect rotLeftRect = new Rect (slotRect.x + (1.1f * tileWidth), slotRect.y + (0.6f * tileHeight), slotRect.width * 0.4f, slotRect.height * 0.4f);
								GUI.Box (slotRect, EMPTY_STRING, skin.GetStyle ("Slot"));	
								Tile tile = slots [i];
								tile = inventory [i];
								if (GUI.Button (rotRightRect, EMPTY_STRING, skin.GetStyle ("Rotate Right"))) {
										tile.RotateRight ();
								}
								if (GUI.Button (rotLeftRect, EMPTY_STRING, skin.GetStyle ("Rotate Left"))) {
										tile.RotateLeft ();
								}
								if (!tile.isEmpty ()) {
										GUI.DrawTexture (tileRect, tile.GetIcon ());
										if (slotRect.Contains (e.mousePosition)) {
												if (e.button == 0 && e.type == EventType.mouseDrag && !draggingTile) {
														draggingTile = true;
														draggedTile = tile;
														inventory [i] = new Tile ();
														prevIdx = i;
												}
//												if (e.type == EventType.mouseUp && draggingTile) {
//														inventory [prevIdx] = inventory [i];
//														inventory [i] = draggedTile;
//														draggingTile = false;
//														draggedTile = null;
//												}
						
												if (!draggingTile) {
														CreateToolTip (tile);
														showToolTip = true;
												}
						
//												if (e.isKey && e.type == EventType.keyDown && (e.character == 'r' || e.character == 'R')) {
//														RotateTile (tile);
//														//TODO implement rotation
//												}
						
												if (Input.GetMouseButtonDown (0)) {
														//highlight box
												}
										}
					
								} else {
										if (currentTileCoord.x == 0 && currentTileCoord.y == 1) {
												if (e.type == EventType.mouseUp && draggingTile) {
														Debug.Log ("reached here");
							
												}
						
										}
					
								}
				
								if (slotRect.Contains (e.mousePosition)) {
										if (e.type == EventType.mouseUp && draggingTile) {
												inventory [i] = draggedTile;
												draggingTile = false;
												draggedTile = null;
						
										}
					
								}
				
				
								if (tooltip == EMPTY_STRING) {
										showToolTip = false;
								}
								i++;
						}
			
				}
				if (draggingTile) {
						GUI.DrawTexture (new Rect (Event.current.mousePosition.x, Event.current.mousePosition.y, tileWidth, tileHeight), draggedTile.GetIcon ());
				}
		}
	
		void CreateToolTip (Tile tile)
		{
				tooltip = "<color=#4DA4BF>" + tile.name + "</color>\n\n";
		}
	
		void AddTile (int id)
		{
		
				for (int i=0; i<inventory.Count; i++) {
						if (inventory [i].isEmpty ()) {	
								inventory [i] = database.tiles [id];
								for (int j=0; j<database.tiles.Count; j++) {
										if (database.tiles [j].ID == id) {
												//if (inventory.Contains (database.tiles [j])) {
												//	inventory[i] = new Tile(database.tiles[i].);
												//} else {
												inventory [i] = database.tiles [j];
												//}
												
										}
					
								}
								break;
						}
				}
		
		}

		public void SetDisabled ()
		{
				disabled = true;

		}
	
	
		void RemoveTile (int id)
		{
		
				for (int i=0; i<inventory.Count; i++) {
						if (inventory [i].ID == id) {
								inventory [i] = new Tile ();
								break;
						}
				}
		
		}
	
		bool InventoryContains (int id)
		{
				for (int i=0; i<inventory.Count; i++) {
						if (inventory [i].ID == id) {
								return true;
						}
				}
				return false;
		}
}
