using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
		public Tile[] inventory;
		public Tile[] slots;
		private TileDB database;
		public int slotsX, slotsY;
		private bool showInventory;
		public GUISkin skin;	
		private static string EMPTY_STRING = "";	
		public bool showToolTip;
		public string tooltip;
		public bool draggingTile;
		public Tile draggedTile;
		public int prevIdx;
		public Event e;
		private float tileWidth = 37.0f;
		private float tileHeight = 37.0f;
		private bool disabled = false;
		private bool displayLabel = false;
		public bool isRestart;

		// Use this for initialization
		void Start ()
		{
				slots = new Tile[slotsX * slotsY];
				inventory = new Tile[slotsX * slotsY];
				database = TileDB.tileDB;
				Load ();
				if (IsEmpty ()) {
						for (int i = 0; i<inventory.Length; i++) {
								int tileId = UnityEngine.Random.Range (0, database.tiles.Count - 1);
								AddTile (tileId);				
						}	
				}
				
		}

		bool IsEmpty ()
		{
				foreach (Tile tile in inventory) {
						if (!tile.isEmpty ()) {
								return false;
						}
				}	
				return true;
		}

		void OnGUI ()
		{

				GUI.skin = skin;

				if (!disabled) {
						DrawInventory ();
				} else {	
						isRestart = GUI.Button (new Rect (Screen.width / 2, Screen.height / 2, 128, 128), EMPTY_STRING);
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

								if (tile != null && !tile.isEmpty ()) {
										if (GUI.Button (rotRightRect, EMPTY_STRING, skin.GetStyle ("Rotate Right"))) {
												tile.RotateRight ();
										}
										if (GUI.Button (rotLeftRect, EMPTY_STRING, skin.GetStyle ("Rotate Left"))) {
												tile.RotateLeft ();
										}
										GUI.DrawTexture (tileRect, tile.GetIcon ());
										if (slotRect.Contains (e.mousePosition)) {
												if (e.button == 0 && e.type == EventType.mouseDrag && !draggingTile) {
														draggingTile = true;
														draggedTile = tile;
														inventory [i] = null;
														prevIdx = i;
												}
												if (e.type == EventType.mouseUp && draggingTile) {
														inventory [prevIdx] = inventory [i];
														inventory [i] = draggedTile;
														draggingTile = true;
												}
						
												if (!draggingTile) {
														CreateToolTip (tile);
														showToolTip = true;
												}
						
												if (Input.GetMouseButtonDown (0)) {
														//highlight box
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
						GUI.DrawTexture (new Rect (Event.current.mousePosition.x - tileWidth / 2, Event.current.mousePosition.y - tileHeight / 2, tileWidth, tileHeight), draggedTile.GetIcon ());
				}
		}
	
		void CreateToolTip (Tile tile)
		{
				tooltip = "<color=#4DA4BF>" + tile.name + "</color>\n\n";
		}
	
		void AddTile (int id)
		{
		
				for (int i=0; i<inventory.Length; i++) {
						if (inventory [i] == null || inventory [i].isEmpty ()) {	
								GameObject DBTile = database.tiles [id];
								inventory [i] = CreateTile (DBTile.name);
								break;
						}
				}
		
		}

		public static Tile CreateTile (string name)
		{
				GameObject newTile = new GameObject ();
				Tile tile = newTile.AddComponent<Tile> ();
				tile.SetUpTile (Tile.getTileType (name));
				return tile;
		}

		public void SetDisabled ()
		{
				disabled = true;

		}

		public void Save ()
		{
				BinaryFormatter bf = new BinaryFormatter ();
				FileStream file = File.Create (Application.persistentDataPath + "/" + Application.loadedLevelName + ".dat");

				PlayerData data = new PlayerData ();
				data.tile0 = new TileData (inventory [0].gameObject.name, 0, inventory [0].gameObject.transform.eulerAngles.z);
				data.tile1 = new TileData (inventory [1].gameObject.name, 1, inventory [1].gameObject.transform.eulerAngles.z);
				data.tile2 = new TileData (inventory [2].gameObject.name, 2, inventory [2].gameObject.transform.eulerAngles.z);
				bf.Serialize (file, data);				
				file.Close ();
		}

		public void Load ()
		{
				BinaryFormatter bf = new BinaryFormatter ();
				string path = Application.persistentDataPath + "/" + Application.loadedLevelName + ".dat";
				if (File.Exists (path)) {
						FileStream file = File.Open (path, FileMode.Open);
						PlayerData data = (PlayerData)bf.Deserialize (file);
						DeserializeTile (data.tile0);
						DeserializeTile (data.tile1);
						DeserializeTile (data.tile2);
						file.Close ();
				}
		}
	
//		bool InventoryContains (int id)
//		{
//				for (int i=0; i<inventory.Count; i++) {
//						if (inventory [i].ID == id) {
//								return true;
//						}
//				}
//				return false;
//		}

		void DeserializeTile (TileData tileData)
		{
				Tile tile0 = CreateTile (tileData.tileType);
				Vector3 rotation = new Vector3 (0.0f, 0.0f, tileData.rotation);
				tile0.gameObject.transform.rotation = Quaternion.Euler (rotation);
				Destroy (inventory [tileData.index]);
				inventory [tileData.index] = tile0;
		}
}

[Serializable]
class TileData
{
		public string tileType;
		public int index;
		public float rotation;

		public TileData (string tileType, int index, float rotation)
		{
				this.tileType = tileType;
				this.index = index;
				this.rotation = rotation;

		}
}

[Serializable]
class PlayerData
{
		public TileData tile0;
		public TileData tile1;
		public TileData tile2;

}
