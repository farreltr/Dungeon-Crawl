using UnityEngine;
using System.Collections;

public class GreenController : PlayerController
{

		// Use this for initialization
		void Start ()
		{
				name = "green";
				respawnPosition = new Vector3 (150.0f, 90.0f, 0); 
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
