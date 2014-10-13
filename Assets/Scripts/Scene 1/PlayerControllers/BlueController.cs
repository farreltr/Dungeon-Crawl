using UnityEngine;
using System.Collections;

public class BlueController : PlayerController
{


		// Use this for initialization
		void Start ()
		{
				name = "blue";
				respawnPosition = new Vector3 (850.0f, 910.0f, 0);
				startDirection = new Vector2 (0.0f, 1.0f);
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
				return "blue";
		}
	
}
