using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile : Object
{

		new public string name;
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
				case "straight-junction":
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

//		public Tile CreateNewTile (TileType tileType)
//		{
//				GameObject tile = new GameObject ();
//				SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer> ();
//				Rigidbody2D rigidbody = tile.AddComponent<Rigidbody2D> ();
//				
//				switch (tileType) {
//				case  TileType.Block:
//						BoxCollider2D block = tile.AddComponent<BoxCollider2D> ();
//						break;
//						
//				case TileType.CrossJunction:
//						Vector2 sizecj = new Vector2 (0.3f, 0.3f);
//						Vector2 ctopleft = new Vector2 (0.35f, 0.35f);
//						Vector2 ctopright = new Vector2 (0.35f, -0.35f);
//						Vector2 cbottomleft = new Vector2 (-0.35f, 0.35f);
//						Vector2 cbottomright = new Vector2 (-0.35f, -0.35f);
//			
//						BoxCollider2D topleft = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D topright = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D bottomleft = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D bottomright = tile.AddComponent<BoxCollider2D> ();
//			
//						topleft.size = sizecj;
//						topright.size = sizecj;
//						bottomleft.size = sizecj;
//						bottomright.size = sizecj;
//			
//						topleft.center = ctopleft;
//						topright.center = ctopright;
//						bottomleft.center = cbottomleft;
//						bottomright.center = cbottomright;
//			
//						break;
//						
//				case TileType.TJunction:
//						Vector2 tjsizetop = new Vector2 (1.0f, 0.35f);
//						Vector2 tjsizebottom = new Vector2 (0.35f, 0.35f);
//						Vector2 tjtop = new Vector2 (0f, 0.32f);
//						Vector2 tjbottomleft = new Vector2 (-0.32f, -0.32f);
//						Vector2 tjbottomright = new Vector2 (0.32f, -0.32f);
//			
//						BoxCollider2D toptj = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D bottomlefttj = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D bottomrighttj = tile.AddComponent<BoxCollider2D> ();
//			
//						toptj.size = tjsizetop;
//						bottomlefttj.size = tjsizebottom;
//						bottomrighttj.size = tjsizebottom;
//			
//						toptj.center = tjtop;
//						bottomlefttj.center = tjbottomleft;
//						bottomrighttj.center = tjbottomright;
//						break;
//						
//				case TileType.Curve:
//						
//				case TileType.Straight:
//						Vector2 sizes = new Vector2 (1.0f, 0.35f);
//						Vector2 ctop = new Vector2 (0f, 0.32f);
//						Vector2 cbottom = new Vector2 (0f, -0.32f);
//			
//						BoxCollider2D top = tile.AddComponent<BoxCollider2D> ();
//						BoxCollider2D bottom = tile.AddComponent<BoxCollider2D> ();
//			
//						top.size = sizes;
//						bottom.size = sizes;
//			
//						top.center = ctop;
//						bottom.center = cbottom;
//			
//						break;
//						
//				default :
//						return TileType.Empty;
//				}
//		
//		}
	
	
	
	
}

