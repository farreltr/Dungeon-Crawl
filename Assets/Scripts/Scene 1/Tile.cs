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
				case "curve":
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

		public void Rotate ()
		{
				this.rotation = Quaternion.Euler (new Vector3 (rotation.x, rotation.y, rotation.z + 90));
		}




}

