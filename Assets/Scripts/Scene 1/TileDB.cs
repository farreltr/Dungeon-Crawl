using UnityEngine;
using System.Collections.Generic;

public class TileDB : MonoBehaviour
{

		public List<GameObject> tiles = new List<GameObject> ();

		public static TileDB tileDB;
	
		void Awake ()
		{
				if (tileDB == null) {
						DontDestroyOnLoad (tileDB);
						tileDB = this;
				} else if (tileDB != this) {
						Destroy (gameObject);
				}
		}

		void Start ()
		{
				DontDestroyOnLoad (gameObject);
				GameObject[] prefabs = Resources.LoadAll<GameObject> ("Tiles/Prefabs/");
				foreach (GameObject prefab in prefabs) {
						tiles.Add (prefab);
				}
		}



}

