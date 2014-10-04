using UnityEngine;
using System.Collections.Generic;

public class TileDB : MonoBehaviour
{

		public List<Tile> tiles = new List<Tile> ();

		void Start ()
		{
				Transform[] prefabs = Resources.LoadAll<Transform> ("Tiles/Prefabs/");
				foreach (Transform prefab in prefabs) {
						tiles.Add (new Tile (prefab));
				}
		}



}

