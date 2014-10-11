using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile : Object
{

		public string name;
		public int ID;
		public Texture2D icon;
		public TileType type;
		public Sprite sprite;
		public Quaternion rotation;
		public GameObject go;
		private static Vector3 ZERO_ANGLE = new Vector3 (0.0f, 0.0f, 0.0f);
		//private static Vector3 NINETY_ANGLE = new Vector3 (0.0f, 0.0f, 90.0f);
		//private static Vector3 ONE_EIGHTY_ANGLE = new Vector3 (0.0f, 0.0f, 180.0f);
		private static Vector3 TWO_SEVENTY_ANGLE = new Vector3 (0.0f, 0.0f, 270.0f);
		//private static Vector3 THREE_SIXTY_ANGLE = new Vector3 (0.0f, 0.0f, 360.0f);

		public enum TileType
		{
				Block,
				CrossJunction,
				TJunction,
				Curve,
				Straight,
				Empty
		}

		public static TileType getTileType (string tileString)
		{
				switch (tileString) {
				case "block":
						return TileType.Block;
				case "cross-junction":
						return TileType.CrossJunction;
				case "t-junction":
						return TileType.TJunction;
				case "right-angle-junction":
						return TileType.Curve;
				case "straight":
						return TileType.Straight;
				default :
						return TileType.Empty;
				}
		}

		public Tile (GameObject go)
		{
				this.go = go;
				this.name = go.name;
				this.sprite = go.GetComponent<SpriteRenderer> ().sprite;
				this.icon = this.sprite.texture;
				this.type = getTileType (sprite.name);
				//this.rotation = Quaternion.Euler (go.transform.localEulerAngles);
				this.rotation = go.transform.rotation;
					
		} 


	
		public Tile ()
		{
				this.type = TileType.Empty;

		}


		public bool isEmpty ()
		{
				return this.type.Equals (TileType.Empty);
		}

		public Texture2D GetIcon ()
		{
				return Resources.Load<Texture2D> ("Tiles/Sprites/" + getRotationString () + "/" + this.name);
		}

		string getRotationString ()
		{
				int rot = Mathf.FloorToInt (this.rotation.eulerAngles.z);
				return string.Concat (rot, "-degree-rotation");
		}

		public void RotateLeft ()
		{
				if (rotation.eulerAngles == TWO_SEVENTY_ANGLE) {
						this.rotation = Quaternion.Euler (ZERO_ANGLE);
				} else {
						this.rotation = Quaternion.Euler (new Vector3 (rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z + 90));
				}

		}

		public void RotateRight ()
		{
				if (rotation.eulerAngles == ZERO_ANGLE) {
						this.rotation = Quaternion.Euler (TWO_SEVENTY_ANGLE);
				} else {
						this.rotation = Quaternion.Euler (new Vector3 (rotation.eulerAngles.x, rotation.eulerAngles.y, rotation.eulerAngles.z - 90));
				}

		}




}

