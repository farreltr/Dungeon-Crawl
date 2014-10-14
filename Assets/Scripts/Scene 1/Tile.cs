using UnityEngine;
using System.Collections;


[System.Serializable]
public class Tile : MonoBehaviour
{
		public TileType type = TileType.Empty;
		private static Vector3 ZERO_ANGLE = new Vector3 (0.0f, 0.0f, 0.0f);
		private static Vector3 TWO_SEVENTY_ANGLE = new Vector3 (0.0f, 0.0f, 270.0f);

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

		public static string getTileString (TileType tileType)
		{
				switch (tileType) {
				case TileType.Block:
						return "block";
				case TileType.CrossJunction:
						return "cross-junction";
				case TileType.TJunction:
						return "t-junction";
				case TileType.Curve:
						return "right-angle-junction";
				case  TileType.Straight:
						return "straight-junction";
				default :
						return "";
				}
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
				int rot = Mathf.FloorToInt (gameObject.transform.rotation.eulerAngles.z);
				return string.Concat (rot, "-degree-rotation");
		}

		public void RotateLeft ()
		{
				if (gameObject.transform.rotation.eulerAngles == TWO_SEVENTY_ANGLE) {
						gameObject.transform.rotation = Quaternion.Euler (ZERO_ANGLE);
				} else {
						gameObject.transform.rotation = Quaternion.Euler (new Vector3 (gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z + 90));
				}

		}

		public void RotateRight ()
		{
				if (gameObject.transform.eulerAngles == ZERO_ANGLE) {
						gameObject.transform.rotation = Quaternion.Euler (TWO_SEVENTY_ANGLE);
				} else {
						gameObject.transform.rotation = Quaternion.Euler (new Vector3 (gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y, gameObject.transform.eulerAngles.z - 90));
				}

		}

		public void SetUpTile (TileType tileType)
		{
				GameObject tile = this.gameObject;
				SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer> ();
				Rigidbody2D rigidbody = tile.AddComponent<Rigidbody2D> ();
				rigidbody.mass = 10.0f;
				rigidbody.drag = 0.0f;
				rigidbody.angularDrag = 0.0f;
				rigidbody.gravityScale = 1.0f;
				rigidbody.fixedAngle = true;
				rigidbody.isKinematic = true;
				rigidbody.interpolation = RigidbodyInterpolation2D.None;
				rigidbody.sleepMode = RigidbodySleepMode2D.StartAsleep;
				rigidbody.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
				tile.name = getTileString (tileType);
				spriteRenderer.sprite = Resources.Load <Sprite> ("Tiles/Sprites/0-degree-rotation/" + tile.name);
				tile.transform.parent = GameObject.FindObjectOfType<Inventory> ().transform;
				this.type = tileType;
				
				switch (tileType) {
				case  TileType.Block:
						BoxCollider2D block = tile.AddComponent<BoxCollider2D> ();
						break;
						
				case TileType.CrossJunction:
						Vector2 sizecj = new Vector2 (0.3f, 0.3f);
						Vector2 ctopleft = new Vector2 (0.35f, 0.35f);
						Vector2 ctopright = new Vector2 (0.35f, -0.35f);
						Vector2 cbottomleft = new Vector2 (-0.35f, 0.35f);
						Vector2 cbottomright = new Vector2 (-0.35f, -0.35f);
			
						BoxCollider2D topleft = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D topright = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottomleft = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottomright = tile.AddComponent<BoxCollider2D> ();
			
						topleft.size = sizecj;
						topright.size = sizecj;
						bottomleft.size = sizecj;
						bottomright.size = sizecj;
			
						topleft.center = ctopleft;
						topright.center = ctopright;
						bottomleft.center = cbottomleft;
						bottomright.center = cbottomright;
			
						break;
						
				case TileType.TJunction:
						Vector2 tjsizetop = new Vector2 (1.0f, 0.32f);
						Vector2 tjsizebottom = new Vector2 (0.3f, 0.3f);
						Vector2 tjtop = new Vector2 (0f, 0.35f);
						Vector2 tjbottomleft = new Vector2 (-0.35f, -0.35f);
						Vector2 tjbottomright = new Vector2 (0.35f, -0.35f);
			
						BoxCollider2D toptj = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottomlefttj = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottomrighttj = tile.AddComponent<BoxCollider2D> ();
			
						toptj.size = tjsizetop;
						bottomlefttj.size = tjsizebottom;
						bottomrighttj.size = tjsizebottom;
			
						toptj.center = tjtop;
						bottomlefttj.center = tjbottomleft;
						bottomrighttj.center = tjbottomright;
						break;
						
				case TileType.Curve:
						BoxCollider2D topRight = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D leftBarrier = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottomBarrier = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D trigger = tile.AddComponent<BoxCollider2D> ();
						
						topRight.size = new Vector2 (0.3f, 0.3f);
						topRight.center = new Vector2 (0.3f, 0.3f);
						leftBarrier.size = new Vector2 (0.32f, 1.0f);
						leftBarrier.center = new Vector2 (-0.33f, 0.0f);
						bottomBarrier.size = new Vector2 (1.0f, 0.32f);
						bottomBarrier.center = new Vector2 (0.0f, -0.33f);
						trigger.size = new Vector2 (0.8f, 0.08f);
						trigger.center = new Vector2 (-0.13f, -0.13f);
						trigger.isTrigger = true;
						break;

				case TileType.Straight:
						Vector2 sizes = new Vector2 (1.0f, 0.3f);
						Vector2 ctop = new Vector2 (0f, 0.35f);
						Vector2 cbottom = new Vector2 (0f, -0.35f);
			
						BoxCollider2D top = tile.AddComponent<BoxCollider2D> ();
						BoxCollider2D bottom = tile.AddComponent<BoxCollider2D> ();
			
						top.size = sizes;
						bottom.size = sizes;
			
						top.center = ctop;
						bottom.center = cbottom;
			
						break;
				}
		
		}
	
}

