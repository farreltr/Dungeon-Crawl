using UnityEngine;
using System.Collections;

public class RedController : PlayerController
{
		// Use this for initialization
		void Start ()
		{
				name = "red";
				respawnPosition = new Vector3 (910.0f, 150.0f, 0);
				startDirection = new Vector2 (-1.0f, 0.0f);
				base.Start ();
		}

		public override string GetName ()
		{
				return name;
		}
		public override Vector3 GetRespawnPosition ()
		{
				return respawnPosition;
		}
		public override string GetRespawnName ()
		{
				return "yellow";
		}
	
	
}
