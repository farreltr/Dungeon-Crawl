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
 
		// Use this for initialization
		void Start ()
		{
				//instantiate sprites and place them on the tile Map?
				animator = this.GetComponent<Animator> ();
				//tileMap = this.GetComponentInParent<TileMap> ();
				tileMap = GameObject.FindGameObjectWithTag ("Tile Map").GetComponent<TileMap> ();
				maxX = tileMap.transform.position.x + tileMap.tileSize + boundary;
				minX = tileMap.transform.position.x + (tileMap.size_x - 1) * tileMap.tileSize + boundary;
				maxY = tileMap.transform.position.y + (tileMap.size_z - 1) * tileMap.tileSize + boundary;
				minY = tileMap.transform.position.y + tileMap.tileSize + boundary;
				//maxX = tileMap.transform.
		}
 
		// Update is called once per frame
		void Update ()
		{
				Vector3 movement = new Vector3 (speed.x * direction.x, speed.y * direction.y, 0);
				movement *= Time.deltaTime;
				transform.Translate (movement);
				OutOfbounds ();
		}

		void setDirection ()
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
				direction = new Vector2 (-direction.x, -direction.y);
				transform.Translate (direction);
		}
	
	
		void OutOfbounds ()
		{
				if (transform.position.x < minX || transform.position.x > maxX) {
						direction = new Vector2 (-direction.x, direction.y);
						transform.Translate (direction);
				}
		
				if (transform.position.y < minY || transform.position.y > maxY) {
						direction = new Vector2 (direction.x, -direction.y);
						transform.Translate (direction);
				}
		
		}

}