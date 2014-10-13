using UnityEngine;
using System.Collections;

public class YellowController : PlayerController
{

	
		// Use this for initialization
		void Start ()
		{
				name = "yellow";
				startDirection = new Vector2 (1.0f, 0.0f);
				respawnPosition = new Vector3 (90.0f, 850.0f, 0);	
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
				return "red";
		}
}
