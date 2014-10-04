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
		public Transform go;

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

		public Tile (Transform go)
		{
				this.go = go;
				this.name = go.name;
				this.sprite = Resources.Load<Sprite> ("Tiles/Sprites/" + name);	
				this.icon = this.sprite.texture;
				this.type = getTileType (sprite.name);
				this.rotation = go.rotation;
					
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
				return icon;
		}




}

