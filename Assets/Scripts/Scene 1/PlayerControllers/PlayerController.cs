using UnityEngine;

public abstract class PlayerController : MonoBehaviour
{
		public Vector2 speed = new Vector2 (20, 20);
		public Vector2 direction;	
		private Animator animator;
		private TileMap tileMap;	
		private float minX;
		private float maxX ;
		private float minY;
		private float  maxY;
		private float boundary = 10.0f;
		public static Vector2 RIGHT = new Vector2 (1.0f, 0.0f);
		public static Vector2 LEFT = new Vector2 (-1.0f, 0.0f);
		public static Vector2 UP = new Vector2 (0.0f, 1.0f);
		public static Vector2 DOWN = new Vector2 (0.0f, -1.0f);
		public static Vector2 RIGHT_UP = new Vector2 (1.0f, 1.0f);
		public static Vector2 LEFT_UP = new Vector2 (-1.0f, 1.0f);
		public static Vector2 RIGHT_DOWN = new Vector2 (1.0f, -1.0f);
		public static Vector2 LEFT_DOWN = new Vector2 (-1.0f, -1.0f);
		public static Vector2 STOP = new Vector2 (0.0f, 0.0f);
		public bool isWinner = false;
		public bool isRespawn = true;
		public Vector2 respawnPosition;
		public Vector3 startDirection;
		new string name;
 
		public void Start ()
		{
				animator = this.GetComponent<Animator> ();
				SetUpBoundaries ();
				direction = startDirection;
				this.transform.position = respawnPosition;
		}

		void SetUpBoundaries ()
		{
				tileMap = GameObject.FindGameObjectWithTag ("Tile Map").GetComponent<TileMap> ();
				minX = tileMap.transform.position.x + tileMap.tileSize + boundary;
				maxX = tileMap.transform.position.x + (tileMap.size_x - 1) * tileMap.tileSize - boundary;
				minY = tileMap.transform.position.y + tileMap.tileSize + boundary;
				maxY = tileMap.transform.position.y + (tileMap.size_y - 1) * tileMap.tileSize - boundary;
		}
 
		// Update is called once per frame
		void Update ()
		{
				Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
				movement *= Time.deltaTime;
				transform.Translate (movement);
				SetDirection ();
				bool isOutOfBounds = IsOutOfbounds ();
				if (isRespawn) {
						animator.enabled = false;
				} else {
						animator.enabled = true;
				}
				if (!isOutOfBounds) {
						isRespawn = false;
				}
				if (!isRespawn && isOutOfBounds) {
						ChangeDirection ();
				}
		
		}

		void SetDirection ()
		{
				if (direction.y > 0) {
						animator.SetInteger ("Direction", 2);
				} else
			if (direction.y < 0) {
						animator.SetInteger ("Direction", 0);
				} else
				if (direction.x > 0) {
						animator.SetInteger ("Direction", 3);
				} else
					if (direction.x < 0) {
						animator.SetInteger ("Direction", 1);
				}
		}

		void OnCollisionEnter2D (Collision2D collision)
		{
				if (!isRespawn) {
						ChangeDirection ();
						PlayerController collisionController = collision.gameObject.GetComponent<PlayerController> ();
						if (collisionController != null) {
								collisionController.ChangeDirection ();
						}
				}

		}

		public void ChangeDirection ()
		{
				if (direction == RIGHT) {
						direction = LEFT;
				} else if (direction == LEFT) {
						direction = RIGHT;
				} else if (direction == UP) {
						direction = DOWN;
				} else if (direction == DOWN) {
						direction = UP;
				}		
				transform.Translate (direction);
		}
	
	
		bool IsOutOfbounds ()
		{
				if (transform.position.x < minX || transform.position.x > maxX) {
						return true;
				}
		
				if (transform.position.y < minY || transform.position.y > maxY) {
						return true;
				}
				return false;
		
		}

		public abstract string GetName ();

		public void Stop ()
		{
				direction = STOP;
				transform.Translate (direction);
		}

		public void TurnRight ()
		{
				if (direction == RIGHT) {
						direction = DOWN;
				} else if (direction == LEFT) {
						direction = UP;
				} else if (direction == UP) {
						direction = RIGHT;
				} else if (direction == DOWN) {
						direction = LEFT;
				}		
				transform.Translate (direction);
		}

		public void Turn45Right ()
		{
				if (direction == RIGHT) {
						direction = RIGHT_DOWN;
				} else if (direction == LEFT) {
						direction = LEFT_UP;
				} else if (direction == UP) {
						direction = RIGHT_UP;
				} else if (direction == DOWN) {
						direction = LEFT_DOWN;
				} else if (direction == RIGHT_DOWN) {
						direction = DOWN;
				} else if (direction == LEFT_DOWN) {
						direction = LEFT;
				} else if (direction == RIGHT_UP) {
						direction = RIGHT;
				} else if (direction == LEFT_UP) {
						direction = UP;
				}
				transform.Translate (direction);
		}

		public void TurnLeft ()
		{
				if (direction == RIGHT) {
						direction = UP;
				} else if (direction == LEFT) {
						direction = DOWN;
				} else if (direction == UP) {
						direction = LEFT;
				} else if (direction == DOWN) {
						direction = RIGHT;
				}		
				transform.Translate (direction);
		
		}

		public void Turn45Left ()
		{

				if (direction == RIGHT) {
						direction = RIGHT_UP;
				} else if (direction == LEFT) {
						direction = LEFT_DOWN;
				} else if (direction == UP) {
						direction = LEFT_UP;
				} else if (direction == DOWN) {
						direction = RIGHT_DOWN;
				} else if (direction == RIGHT_DOWN) {
						direction = RIGHT;
				} else if (direction == LEFT_DOWN) {
						direction = DOWN;
				} else if (direction == RIGHT_UP) {
						direction = UP;
				} else if (direction == LEFT_UP) {
						direction = LEFT;
				}
				transform.Translate (direction);
		
		}

		public bool checkRespawn (int x, int y)
		{
				int x_idx = Mathf.FloorToInt (GetPosition ().x / tileMap.tileSize) - 1;
				int y_idx = Mathf.FloorToInt (GetPosition ().y / tileMap.tileSize) - 1;
				return x == x_idx && y == y_idx;
		}

		public bool isOnRow (int y)
		{
				return y.Equals (Mathf.FloorToInt (GetPosition ().y / tileMap.tileSize));
		}

		public bool isOnColumn (int x)
		{
				return x.Equals (Mathf.FloorToInt (GetPosition ().x / tileMap.tileSize));
		}
	
		public void respawn ()
		{
				this.transform.position = respawnPosition;
				direction = startDirection;
				transform.Translate (direction);
				isRespawn = true;
		}

		public abstract Vector3 GetRespawnPosition ();
		public abstract string GetRespawnName ();

		public Vector3 GetPosition ()
		{
				return transform.position;

		}

		public void ShiftRight (int x, int y)
		{
				if (this.isOnRow (y + 1)) {
						if (checkRespawn (x, y)) {
								respawn ();
						} else {
								if (!isRespawn) {
										transform.position = new Vector3 (GetPosition ().x + 100.0f, GetPosition ().y, 0.0f);
								}
						}
				}
		}

		public void ShiftUp (int x, int y)
		{
				if (isOnColumn (x + 1)) {
						if (checkRespawn (x, y)) {
								respawn ();
						} else {
								if (!isRespawn) {
										transform.position = new Vector3 (GetPosition ().x, GetPosition ().y + 100.0f, 0.0f);
								}
						}
				}
		}

		public void ShiftLeft (int x, int y)
		{
				if (isOnRow (y + 1)) {
						if (checkRespawn (x, y)) {
								respawn ();
						} else {
								if (!isRespawn) {
										transform.position = new Vector3 (GetPosition ().x - 100.0f, GetPosition ().y, 0.0f);
								}
						}
				}
		}

		public void ShiftDown (int x, int y)
		{
				if (isOnColumn (x + 1)) {
						if (checkRespawn (x, y)) {
								respawn ();
						} else {
								if (!isRespawn) {
										transform.position = new Vector3 (GetPosition ().x, GetPosition ().y - 100.0f, 0.0f);
								}
						}
				}
		}
}