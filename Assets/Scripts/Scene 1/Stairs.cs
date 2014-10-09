using UnityEngine;
using System.Collections;

public class Stairs : MonoBehaviour
{

		// Use this for initialization
		void Start ()
		{
				GameObject[] stairs = Resources.LoadAll<GameObject> ("Stairs/Prefabs/");
				foreach (GameObject staircase in stairs) {
						GameObject s = (GameObject)Instantiate (staircase);
						s.transform.parent = this.transform;
						SpriteRenderer renderer = s.GetComponent<SpriteRenderer> ();
						renderer.sortingLayerID = 6;
						renderer.sortingOrder = 0;
				}
		
		}
	
		// Update is called once per frame
		void Update ()
		{
	
		}
}
