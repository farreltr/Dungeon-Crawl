using UnityEngine;
using System.Collections.Generic;

public class TileDB : MonoBehaviour
{

		public List<Tile> tiles = new List<Tile> ();

		void Start ()
		{
				GameObject[] prefabs = Resources.LoadAll<GameObject> ("Tiles/Prefabs/");
				foreach (GameObject prefab in prefabs) {
						tiles.Add (new Tile (prefab));
				}
		}



}

