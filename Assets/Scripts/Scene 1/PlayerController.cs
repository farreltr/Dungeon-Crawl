using UnityEngine;

public class PlayerController : MonoBehaviour
{
		public Vector2 speed = new Vector2 (5, 5);
		public Vector2 direction = new Vector2 (0, 0);	
		private Animator animator;
		private TileMap tileMap;	
		private float minX;
		private float maxX ;
		private float minY;
		private float  maxY;
		private float boundary = 20.0f;
		private static Vector2 RIGHT = new Vector2 (1.0f, 0.0f);
		private static Vector2 LEFT = new Vector2 (-1.0f, 0.0f);
		private static Vector2 UP = new Vector2 (0.0f, 1.0f);
		private static Vector2 DOWN = new Vector2 (0.0f, -1.0f);
 
		void Start ()
		{
				animator = this.GetComponent<Animator> ();
				SetUpBoundaries ();
		}

		void SetUpBoundaries ()
		{
				tileMap = GameObject.FindGameObjectWithTag ("Tile Map").GetComponent<TileMap> ();
				minX = tileMap.transform.position.x + tileMap.tileSize + boundary;
				maxX = tileMap.transform.position.x + (tileMap.size_x - 1) * tileMap.tileSize + boundary;
				minY = tileMap.transform.position.y + tileMap.tileSize + boundary;
				maxY = tileMap.transform.position.y + (tileMap.size_z - 1) * tileMap.tileSize + boundary;
		}
 
		// Update is called once per frame
		void Update ()
		{
				Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
				movement *= Time.deltaTime;
				transform.Translate (movement);
				SetDirection ();
				OutOfbounds ();
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
				ChangeDirection ();
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
	
	
		void OutOfbounds ()
		{
				if (transform.position.x < minX || transform.position.x > maxX) {
						ChangeDirection ();
				}
		
				if (transform.position.y < minY || transform.position.y > maxY) {
						ChangeDirection ();
				}
		
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
	
}